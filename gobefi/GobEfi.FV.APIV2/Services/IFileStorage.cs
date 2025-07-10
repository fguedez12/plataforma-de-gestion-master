using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Services
{
    public interface IFileStorage
    {
        Task DeleteFile(string route, string container);
        Task<string> SaveFile(IFormFile file, string extension, string container, string contentType);
    }
}
