using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ArchivoAdjuntoModels;
using GobEfi.Web.Models.TipoArchivoModels;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GobEfi.Web.Services
{
    public class ArchivoAdjuntoService : IArchivoAdjuntoService
    {
        private readonly IMapper _mapper;
        private readonly IArchivoAdjuntoRepository _repoArchivoAdjunto;
        private readonly ITipoArchivoRepository _repoTipoArchivo;
        private readonly ILogger _logger;

        public ArchivoAdjuntoService(ILoggerFactory loggerFactory, IMapper mapper, IArchivoAdjuntoRepository repoArchivoAdjunto, ITipoArchivoRepository repoTipoArchivo, ICompraRepository repoCompra)
        {
            _mapper = mapper;
            _repoArchivoAdjunto = repoArchivoAdjunto;
            _repoTipoArchivo = repoTipoArchivo;
            _logger = loggerFactory.CreateLogger<ArchivoAdjuntoService>();
        }

        public async Task<long> Add<T>(T entity) where T : class
        {
            var archivoAdjuntoForRegister = entity as ArchivoAdjuntoForRegister;

            var archivo = _mapper.Map<ArchivoAdjunto>(entity);

            //new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider().TryGetContentType(archivo.Nombre, out string mimeType);

            var tipoArchivo = _repoTipoArchivo.All().Where(c => c.Extension == archivoAdjuntoForRegister.ext).FirstOrDefault();


            archivo.CreatedAt = DateTime.Now;
            archivo.Active = true;
            archivo.Version = 1;
            archivo.TipoArchivoId = tipoArchivo.Id;
            
            
            _repoArchivoAdjunto.Add(archivo);
            await _repoArchivoAdjunto.SaveAllAsync();

            return archivo.Id;
        }

        public IEnumerable<ArchivoAdjuntoForRegister> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public ArchivoAdjuntoForRegister Get(long id)
        {
            var file = _repoArchivoAdjunto.Query().Where(aa => aa.Id == id).FirstOrDefault();

            return _mapper.Map<ArchivoAdjuntoForRegister>(file);
        }

        public async Task<ArchivoAdjuntoModel> GetByFacturaId(long facturaId)
        {
            var archivo = await _repoArchivoAdjunto.Query().Where(aa => aa.Id == facturaId).Include(aa => aa.TipoArchivo).FirstOrDefaultAsync();

            return _mapper.Map<ArchivoAdjuntoModel>(archivo);
        }

        public async Task<ICollection<TipoArchivoModel>> GetExtPermitidasFacturaAsync()
        {
            var tipoArchivos = await _repoTipoArchivo.Query().Where(ta => ta.FormatoFactura).ToListAsync();


            return _mapper.Map<ICollection<TipoArchivoModel>>(tipoArchivos);
        }

        public long Insert(ArchivoAdjuntoForRegister model)
        {
            throw new NotImplementedException();
        }

        public long Update(ArchivoAdjuntoForEdit model)
        {
            var archivoOrginal = _repoArchivoAdjunto.Query().Where(aa => aa.Id == model.Id).FirstOrDefault();

            var archivo = _mapper.Map<ArchivoAdjunto>(model);

            var fileExtensionObject = new FileExtensionContentTypeProvider();

            if (!fileExtensionObject.TryGetContentType(archivo.Nombre, out string mimeType))
                throw new Exception("Error al obtener el mimeType del archivo");

            var tipoArchivo = _repoTipoArchivo.All().Where(c => c.MimeType == mimeType).FirstOrDefault();

            archivo.TipoArchivoId = tipoArchivo.Id;
            archivo.UpdatedAt = DateTime.Now;
            archivo.CreatedAt = archivoOrginal.CreatedAt;
            archivo.Active = archivoOrginal.Active;
            archivo.Version = ++archivoOrginal.Version;
            
            _repoArchivoAdjunto.Update(archivo);
            //if (_repoArchivoAdjunto.SaveChanges() > 0)
            //{
                //if (archivoOrginal.Url != archivo.Url)
                //{
                    //if (File.Exists(archivoOrginal.Url))
                    //    File.Delete(archivoOrginal.Url);
                //}
            //}

            return _repoArchivoAdjunto.SaveChanges();
        }

        public void Update(ArchivoAdjuntoForRegister model)
        {
            throw new NotImplementedException();
        }

        public bool ValidaArchivoParaCompra(string ext)
        {
            var formatosParaCompra = _repoTipoArchivo.Query().Where(ta => ta.FormatoFactura).ToList();

            return formatosParaCompra.Find(ta => ta.Extension.ToLower().Contains(ext)) != null;

        }
    }
}