using Microsoft.AspNetCore.Http;

namespace translate_app.Domain.Services;

public interface IFileUploadService
{
    Task<FileUploadResult> UploadFileAsync(IFormFile file);
}