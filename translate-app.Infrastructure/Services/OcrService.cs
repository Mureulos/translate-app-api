using Tesseract;
using Microsoft.Extensions.Configuration;
using translate_app.Domain.Services;

namespace translate_app.Infrastructure.Services;

public sealed class OcrService(IConfiguration configuration) : IOcrService
{
    private readonly string _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
    private readonly string _language = "por+eng";

    public Task<string> ExtractTextAsync(string filePath)
    {
        return Task.Run(() =>
        {
            using var engine = new TesseractEngine(_dataPath, _language, EngineMode.Default);
            using var img = Pix.LoadFromFile(filePath);
            using var page = engine.Process(img);

            return page.GetText();
        });
    }
}