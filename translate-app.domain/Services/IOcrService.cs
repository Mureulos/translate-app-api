namespace translate_app.Domain.Services;

public interface IOcrService
{
    Task<string> ExtractTextAsync(string filePath);
}