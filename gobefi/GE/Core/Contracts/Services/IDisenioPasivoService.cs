using GobEfi.Web.Models.DisenioPasivoModels;
using GobEfi.Web.Models.EdificioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IDisenioPasivoService
    {
       
        //Task<PasoUnoForSave> GetPasoUnoByDivisionId(long id);
        Task PostPasoTres(long id, PasoTresModel model);
        Task<DivisionModel> GetDivision(long id);
        Task<long> Update(long id, PasoUnoData pasoUno);
        Task PostSuelosLv2(long id, DivisionModel model);
        Task LevelPaso3(long id);
        Task PostMurosLv2(long id, DivisionModel model);
        Task PostVentanasLv2(long id, DivisionModel model);
        Task<ArchivoDpModel> PostFile(ArchivoDpModel model);
        Task DeleteFile(long id);
        Task UpdateFile(ArchivoDpModel model);
        Task<long?> setFrontis(long id, long muroId);
        Task<ArchivoDpModel> GetFileById(long id);
        Task<PasoUnoData> GetPasoUnoData(long id);
        Task<PasoDosData> GetPasoDosData(long id);
        Task PasoDosComplete(long id);
        Task<PasoTresModel> GetPasoTresData(long id);
        Task<PasoCuatroData> GetPasoCuatroData(long id);
        Task<PasoTresModel> GetPasoTresDataV2(long id);
        Task PostPasoTresV2(long id, PasoTresModel model);
    }
}
