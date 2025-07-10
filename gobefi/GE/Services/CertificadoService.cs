using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CertificadoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class CertificadoService : ICertificadoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly Usuario _currentUser;

        public CertificadoService(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }
        public async Task<List<CertificadoModel>> GetCertificados()
        {
            var certificadosDb = await _context.Certificados.Where(c => c.Active).ToListAsync();
            var result = _mapper.Map<List<CertificadoModel>>(certificadosDb);

            return result;
        }


        public async Task PostNotas(List<NotaModel> notas)
        {
            try
            {
                var lisNotas = _mapper.Map<List<NotasCertificado>>(notas);

                foreach (var nota in lisNotas)
                {
                    var user = await _userManager.FindByIdAsync(nota.UsuarioId);

                    if (user != null)
                    {
                        nota.CreatedAt = DateTime.Now;
                        nota.CreatedBy = _currentUser.Id;
                        nota.Version = 1;

                        _context.NotasCertificados.Add(nota);
                    }

                   
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }

        public async Task<NotasPagingModel> ListNotas(bool isAdmin,int page,string filtroNombre, string filtroCorreo, int? filtroMinisterio, int? filtroServicio)
        {


            try
            {
                var query = _context.NotasCertificados.Include(n => n.Certificado).Include(n=>n.Usuario)
                   .OrderByDescending(n => n.NotaFinal)
                   .ThenByDescending(n => n.FechaEntrega);

                if (!string.IsNullOrEmpty(filtroNombre)) {
                    query = query.Where(q => q.Usuario.Nombres.Contains(filtroNombre) || q.Usuario.Apellidos.Contains(filtroNombre))
                        .OrderByDescending(n => n.NotaFinal)
                        .ThenByDescending(n => n.FechaEntrega); 
                }
                if (!string.IsNullOrEmpty(filtroCorreo))
                {
                    query = query.Where(q => q.Email.Contains(filtroCorreo))
                        .OrderByDescending(n => n.NotaFinal)
                        .ThenByDescending(n => n.FechaEntrega);
                }
                if (filtroMinisterio!=null)
                {
                    query = query.Where(q => q.Usuario.UsuariosInstituciones.Any(ui=>ui.InstitucionId== filtroMinisterio))
                        .OrderByDescending(n => n.NotaFinal)
                        .ThenByDescending(n => n.FechaEntrega);
                }
                if (filtroServicio != null)
                {
                    query = query.Where(q => q.Usuario.UsuariosServicios.Any(ui => ui.ServicioId == filtroServicio))
                        .OrderByDescending(n => n.NotaFinal)
                        .ThenByDescending(n => n.FechaEntrega);
                }

                if (!isAdmin)
                {

                    var servicioId = await _context.UsuariosServicios.Where(us => us.UsuarioId == _currentUser.Id).Select(us => us.ServicioId).ToListAsync();
                    var usuariosId = await _context.UsuariosServicios.Where(u=>servicioId.Contains(u.ServicioId)).Select(u=>u.UsuarioId).ToListAsync();
                    query = query.Where(q => usuariosId.Contains(q.UsuarioId))
                                .OrderByDescending(n => n.NotaFinal)
                                .ThenByDescending(n => n.FechaEntrega); ;
                }


               
                var list = page==0 ? null : await PagingList.CreateAsync(query, 5, page);

                    
                var listToReturn = page==0 ? _mapper.Map<List<NotaModel>>(await query.ToListAsync()): _mapper.Map<List<NotaModel>>(list);

                //var normName = NormaliceNames(_currentUser.Nombres, _currentUser.Apellidos);

                var ministeriosList = new List<MinisterioToListModel>();
                if (isAdmin)
                {

                    var ministeriosFromDb = await _context.Instituciones.ToListAsync();
                    ministeriosList = _mapper.Map<List<MinisterioToListModel>>(ministeriosFromDb);

                }
                else {
                    var ministeriosFromDb = await _context.UsuariosInstituciones
                        .Include(ui => ui.Institucion)
                        .Where(ui => ui.UsuarioId == _currentUser.Id).Select(ui => ui.Institucion).ToListAsync();
                    ministeriosList = _mapper.Map<List<MinisterioToListModel>>(ministeriosFromDb);
                }

                var serviciosList = new List<ServicioToListModel>();

                if (isAdmin)
                {
                    var serviciosFromDb = await _context.Servicios.ToListAsync();
                    serviciosList = _mapper.Map<List<ServicioToListModel>>(serviciosFromDb);
                }
                else
                {
                    var serviciosFromDb = await _context.UsuariosServicios
                        .Include(us => us.Servicio)
                        .ThenInclude(s=>s.Institucion)
                        .Where(us => us.UsuarioId == _currentUser.Id).Select(us => us.Servicio).ToListAsync();
                    serviciosList = _mapper.Map<List<ServicioToListModel>>(serviciosFromDb);
                }


                foreach (var nota in listToReturn)
                {
                    var usuarioFromDb = await _userManager.FindByIdAsync(nota.UsuarioId);
                    var usuarioServicio = await _context.UsuariosServicios.Where(us => us.UsuarioId == usuarioFromDb.Id).FirstOrDefaultAsync();
                    nota.NombreUsuario = NormaliceNames(usuarioFromDb.Nombres, usuarioFromDb.Apellidos);
                    nota.Email = usuarioFromDb.Email;
                    nota.Nombres = usuarioFromDb.Nombres;
                    nota.Apellidos = usuarioFromDb.Apellidos;
                    var rol = await _userManager.GetRolesAsync(usuarioFromDb);
                    nota.TipoGestor = rol.FirstOrDefault();
                    nota.ServicioId = usuarioServicio.ServicioId;

                    //var ministerioFromDb = await _context.UsuariosInstituciones
                    //    .Include(ui => ui.Institucion)
                    //    .Where(ui => ui.UsuarioId == nota.UsuarioId).Select(ui => ui.Institucion).FirstOrDefaultAsync();

                    //if (ministerioFromDb != null) {
                    //    nota.Ministerio = ministerioFromDb.Nombre;
                    //    if (!ministeriosList.Any(x => x.Id == ministerioFromDb.Id))
                    //    {
                    //        ministeriosList.Add(_mapper.Map<MinisterioToListModel>(ministerioFromDb));
                    //    }
                    //}
                    


                    //var servicioFromDb = await _context.UsuariosServicios
                    //    .Include(us => us.Servicio)
                    //    .ThenInclude(s=>s.Institucion)
                    //    .Where(us => us.UsuarioId == nota.UsuarioId).Select(us => us.Servicio).FirstOrDefaultAsync();

                    //if (servicioFromDb != null) {
                    //    nota.Servicio = servicioFromDb.Nombre;
                    //    if (!serviciosList.Any(x => x.Id == servicioFromDb.Id))
                    //    {
                    //        serviciosList.Add(_mapper.Map<ServicioToListModel>(servicioFromDb));
                    //    }
                    //} 

                }


                var response = new NotasPagingModel()
                {
                    Notas = listToReturn,
                    PageIndex = page==0? 0: list.PageIndex,
                    StartPageIndex = page == 0 ? 0 : list.StartPageIndex,
                    StopPageIndex = page == 0 ? 0 : list.StopPageIndex,
                    Ministerios = ministeriosList,
                    Servicios = serviciosList
                };

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async  Task<List<NotaModel>> ListNotasByUser() 
        {
            try
            {
                var list = await  _context.NotasCertificados.Where(n => n.UsuarioId == _currentUser.Id).Include(n=>n.Certificado)
                    .OrderByDescending(n => n.NotaFinal)
                    .ThenByDescending(n=>n.FechaEntrega)
                    .GroupBy(n => n.CertificadoId)
                    .Select(n => n.FirstOrDefault()).ToListAsync();
                var listToReturn = _mapper.Map<List<NotaModel>>(list);

                var normName = NormaliceNames(_currentUser.Nombres, _currentUser.Apellidos);

                foreach (var nota in listToReturn)
                {
                    nota.NombreUsuario = normName;
                }

                return listToReturn;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private string NormaliceNames(string nombres, string apellidos) 
        {
            try
            {

                string normalized;
                var list = new List<string>();
                var lisnames = nombres.Trim().Split(" ");
                var lisAp = apellidos.Trim().Split(" ");

                if (lisnames.Length > 1)
                {

                    var nombreMin = lisnames[0].ToLower();
                    var st = nombreMin.Substring(0, 1);
                    var rest = nombreMin.Substring(1, nombreMin.Length - 1);
                    var norm = st.ToUpper() + rest;
                    list.Add(norm);

                    var nombreMin2 = lisnames[1].ToLower();
                    if (!string.IsNullOrWhiteSpace(nombreMin2))
                    {
                        var st2 = nombreMin2.Substring(0, 1);

                        var norm2 = st2.ToUpper() + ".";
                        list.Add(norm2);
                    }

                }
                if (lisnames.Length == 1)
                {
                    var nombreMin = lisnames[0].ToLower();
                    var st = nombreMin.Substring(0, 1);
                    var rest = nombreMin.Substring(1, nombreMin.Length - 1);
                    var norm = st.ToUpper() + rest;
                    list.Add(norm);
                }

                if (lisAp.Length > 1) {
                    var apMin = lisAp[0].ToLower();
                    var st = apMin.Substring(0, 1);
                    var rest = apMin.Substring(1, apMin.Length - 1);
                    var norm = st.ToUpper() + rest;
                    list.Add(norm);
                    var apMin2 = lisAp[1].ToLower();
                    if (!string.IsNullOrWhiteSpace(apMin2))
                    {
                        var st2 = apMin2.Substring(0, 1);

                        var norm2 = st2.ToUpper() + ".";
                        list.Add(norm2);
                    }
                }
                if (lisAp.Length == 1)
                {
                    var apMin = lisAp[0].ToLower();
                    var st = apMin.Substring(0, 1);
                    var rest = apMin.Substring(1, apMin.Length - 1);
                    var norm = st.ToUpper() + rest;
                    list.Add(norm);
                }

                normalized = string.Join(" ", list);

                return normalized;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message );
            }
           
        }
    }
}
