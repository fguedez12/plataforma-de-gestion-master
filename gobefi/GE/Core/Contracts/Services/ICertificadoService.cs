using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CertificadoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface ICertificadoService
    {
        Task<List<CertificadoModel>> GetCertificados();
        Task<NotasPagingModel> ListNotas(bool isAdmin,int page, string filtroNombre, string filtroCorreo, int? filtroMinisterio, int? filtroServicio);
        Task<List<NotaModel>> ListNotasByUser();
        Task PostNotas(List<NotaModel> notas);
    }
}
