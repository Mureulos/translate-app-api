using Microsoft.AspNetCore.Http;
using translate_app.Domain;
using translate_app.Domain.Services;

namespace translate_app.Infrastructure.Services;

public sealed class ExtractTextService(IOcrService ocrService): IExtractTextService
{
    private readonly string[] _allowedExtensions = new[] { ".txt", ".png", ".pdf", ".jpg" };
    private readonly IOcrService _ocrService = ocrService;    
    
    public async Task<FileUploadResult> ExtractFileAsync(IFormFile file)
    {
        var validationResult = ValidateFile(file);
        if (!validationResult.Success) return validationResult;

        var fullPath = await SaveTempFileAsync(file);

        try
        {
            string extractedText = Path.GetExtension(fullPath) == ".txt"
                ? await File.ReadAllTextAsync(fullPath)
                : await _ocrService.ExtractTextAsync(fullPath);
            
            return new FileUploadResult 
            { 
                Success = true, 
                FilePath = fullPath, 
                ExtractedContent = extractedText 
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error processing file: " + ex.Message);
        }
        finally
        {
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
    }
    
    private FileUploadResult ValidateFile(IFormFile file)
    {
        if (file == null! || file.Length == 0)
            return new FileUploadResult { Success = false, ErrorMessage = "File not found!" };

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return !_allowedExtensions.Contains(extension) 
            ? new FileUploadResult { Success = false, ExtractedContent = "Invalid file type" }
            : new FileUploadResult { Success = true };
    }
    
    private async Task<string> SaveTempFileAsync(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fullPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{extension}");
        
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        return fullPath;
    }
}