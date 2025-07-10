using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Web.Models.PisoModels;
using GobEfi.Web.Models.UnidadModels;
using GobEfi.Web.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDivisionService : IService<DivisionModel, long>
    {
        void Update(DivisionEditInfGeneralModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DivisionVerModel Ver(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedList<DivisionListModel>> PagedAsync(DivisionRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<DivisionVerModel> Export(DivisionRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<DivisionSelectModel> Select();


        bool SePuedenAsociar(string usuarioId, long divisionId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<DivisionListModel>> AllByUser(string id);
        Task<DivisionModel> GetAsync(long divisionId);
        Task<IEnumerable<DivisionListModel>> GetByServicioId(long servicioId);
        Task<IEnumerable<DivisionListModel>> GetByEdificioId(long edificioId);
        Task<int> AsociarUsuario(string usuarioId, long divisionId);
        Task<IEnumerable<DivisionListModel>> GetNoAsociadosByUserId(string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">string</param>
        /// <param name="institucionId">long</param>
        /// <returns></returns>
        Task<IEnumerable<DivisionListModel>> AllByUserAndInstitucion(string userId, long institucionId);
        Task<int> EliminarAsociacionConUsuario(string usuarioId, long divisionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        Task<IEnumerable<DivisionListModel>> AllByUserAndServicio(string userId, long servicioId);
        Task<IEnumerable<DivisionListModel>> GetByFilters(string userId, long servicioId, long? regionId);
        Task<IEnumerable<DivisionListModel>> GetAsociadosByUserId(string userId);
        Task<long> InsertAsync(DivisionCreateModel division);

        DivisionDeleteModel GetForDelete(long id);
        DivisionEditModel GetForEdit(long id);
        Task Update(DivisionEditModel divisionUpdate); // Cambiado de void a Task
        Task<IEnumerable<DivisionListModel>> GetNoAsociados(string userId, long servicioId);
        Task<ICollection<DivisionesToAssociate>> GetToAssociate(ICollection<long> serviciosId, string userId);
        Task<bool> RoleActualPermiteAsociar();
        Task<IEnumerable<DivisionListModel>> GetByServicioRegion(long? servicioId, long regionId, bool pmg);
        bool UpdateDir(long edificioId, string edificioCalle, string edificioNumero, string nombreRegion);
        Task SetPisosIguales(long divisioId, bool value);
        void DetailsUpdate(DivisionDetailsModel model);
        Task<UnidadReporteConsumoModel> GetReporteConsumo(string id);
        Task JustificaReporteConsumo(JustificaModel model);
        Task<ICollection<UnidadToAssociate>> GetToAssociatev2(ICollection<long> serviciosId, string userId);
        Task<bool> SePuedenAsociarv2(string usuarioId, long unidadId);
        Task<int> AsociarUsuariov2(string usuarioId, long unidadId);
        Task<int> EliminarAsociacionConUsuariov2(string usuarioId, long unidadId);
        Task ObservarJustificacion(JustificaModel model, string userId);
        Task ValidarJustificacion(JustificaModel model, string userId);

        //Task<List<PisoModel>> GetDivisionPisos(long divisionId);

        /// <summary>
        /// Aplica las reglas de negocio para ReportaPMG, IndicadorEE y ReportaEV.
        /// </summary>
        /// <param name="division">La entidad Division a modificar.</param>
        /// <param name="esCreacion">Indica si la operación es una creación (true) o una edición (false).</param>
        /// <returns>Task</returns>
        Task AplicarReglasNegocio(Division division, bool esCreacion);
    }
}
