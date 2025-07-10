using api_gestiona.DTOs.Files;

namespace api_gestiona.Services.Contracts
{
    public interface IFileService
    {
        Task DeleteFile(string url);
        FileDTO SaveFile(IFormFile file);
    }
}
