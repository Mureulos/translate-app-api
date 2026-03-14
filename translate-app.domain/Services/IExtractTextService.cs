using Microsoft.AspNetCore.Http;

namespace translate_app.Domain.Services;

public interface IExtractTextService
{
    Task<FileUploadResult> ExtractFileAsync(IFormFile file);
}