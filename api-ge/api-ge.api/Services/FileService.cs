using api_gestiona.DTOs.Documentos;
using api_gestiona.DTOs.Files;
using api_gestiona.Security;
using api_gestiona.Services.Contracts;
using System.Security.Principal;

namespace api_gestiona.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        public FileDTO SaveFile(IFormFile file)
        {
            string tokenName = Guid.NewGuid().ToString();
            var ruta = _configuration.GetValue<string>("DirectorioArchivosEV:documentos");
            var userName = _configuration.GetValue<string>("ImpersonalizacionSettings:Usuario");
            var password = _configuration.GetValue<string>("ImpersonalizacionSettings:Clave");
            var domain = _configuration.GetValue<string>("ImpersonalizacionSettings:Dominio");
            var ext = Path.GetExtension(file.FileName);
            string nombreArchivo = $"{ruta}\\{tokenName}{ext}";

            using (WindowsLogin wl = new WindowsLogin(userName, domain, password))
            {
                WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {
                    using (var stream = new FileStream(nombreArchivo, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    };
                });
            };

            var response = new FileDTO { Nombre = nombreArchivo, NombreOriginal = file.FileName };

            return response;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        public async Task DeleteFile(string url)
        {
            var userName = _configuration.GetValue<string>("ImpersonalizacionSettings:Usuario");
            var password = _configuration.GetValue<string>("ImpersonalizacionSettings:Clave");
            var domain = _configuration.GetValue<string>("ImpersonalizacionSettings:Dominio");

            
            using (WindowsLogin wl = new WindowsLogin(userName, domain, password))
            {
                WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {
                    if (File.Exists(url))
                    {
                        File.Delete(url);
                    }
                });
            };

        }

    }
}
