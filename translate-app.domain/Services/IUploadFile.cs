using Microsoft.AspNetCore.Http;

namespace translate_app.Domain.Services;

public interface IUploadFile
{
    Task<FileUploadResult> ExtractFileAsync(IFormFile file);
}