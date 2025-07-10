using GobEfi.Business.Security;
using GobEfi.FV.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Services
{
    public class FileStorage : IFileStorage
    {
        private readonly IConfiguration _configuration;
        private readonly ImpersonalizacionSettings _impersonalizacionSettings;

        public FileStorage(IConfiguration configuration, ImpersonalizacionSettings impersonalizacionSettings)
        {
            _configuration = configuration;
            _impersonalizacionSettings = impersonalizacionSettings;
        }
        public async Task<string> SaveFile(IFormFile file, string extension, string container, string contentType)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";
            var folderName = Path.Combine(_configuration.GetValue<string>("DirectorioArchivos:Folder"), container);
            var folder = Path.Combine(_configuration.GetValue<string>("DirectorioArchivos:Server"), folderName);
            var publico = Path.Combine(_configuration.GetValue<string>("DirectorioArchivos:Publico", "flotavehicular"));

            using (WindowsLogin wl = new WindowsLogin(_impersonalizacionSettings.Usuario, _impersonalizacionSettings.Dominio, _impersonalizacionSettings.Clave))
            {

                System.Security.Principal.WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string ruta = Path.Combine(folder, fileName);
                    using (var stream = new FileStream(ruta, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                });

            };

            var urlToDb = Path.Combine(publico, container, fileName).Replace("\\", "/");
            return await Task.FromResult(urlToDb);
        }


        public Task DeleteFile(string route, string container)
        {

            using (WindowsLogin wl = new WindowsLogin(_impersonalizacionSettings.Usuario, _impersonalizacionSettings.Dominio, _impersonalizacionSettings.Clave))
            {

                System.Security.Principal.WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {

                    var folderName = Path.Combine(_configuration.GetValue<string>("DirectorioArchivos:Folder"), container);
                    var folder = Path.Combine(_configuration.GetValue<string>("DirectorioArchivos:Server"), folderName);
                    if (route != null)
                    {
                        var fileName = Path.GetFileName(route);
                        string fileDirectory = Path.Combine(folder, fileName);
                        if (File.Exists(fileDirectory))
                        {
                            File.Delete(fileDirectory);
                        }

                    }

                });

            };


            return Task.FromResult(0);
        }
    }
}
