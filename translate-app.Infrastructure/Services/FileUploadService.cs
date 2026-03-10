using Microsoft.AspNetCore.Http;
using translate_app.Domain;
using translate_app.Domain.Services;

namespace translate_app.Infrastructure.Services;

public sealed class FileUploadService() : IFileUploadService
{
    private readonly string[] ALLOWED_EXTENSIONS = new[] { ".txt", ".png", ".pdf", ".jpg" };
        
    public async Task<FileUploadResult> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return new FileUploadResult { Success = false, ErrorMessage = "File not founded!" };

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!ALLOWED_EXTENSIONS.Contains(extension))
            return new FileUploadResult { Success = false, ErrorMessage = "Invalid type file" };

        var tempPath = Path.GetTempPath();
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(tempPath, uniqueFileName);
        
        using (var stream = new FileStream(fullPath, FileMode.Create))
            await file.CopyToAsync(stream);

        return new FileUploadResult { Success = true, FilePath = fullPath };
    }
}