using GenerativeAI;
using Microsoft.Extensions.Configuration;

namespace translate_app.Infrastructure.Services
{
    public class AIService
    {
        private readonly GenerativeModel _model;

        public AIService(IConfiguration configuration)
        {
            var apiKey = configuration["Gemini:ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("API Key do Gemini não encontrada.");

            _model = new GenerativeModel(apiKey, "gemini-2.0-flash");
        }

        public async Task<string> TranslateAsync(
            string text,
            string targetLanguage,
            string? sourceLanguage = null,
            CancellationToken cancellationToken = default)
        {
            string prompt = sourceLanguage == null
                ? $"Identifique o idioma de origem e traduza o seguinte texto para o idioma {targetLanguage}: \"{text}\""
                : $"Traduza o seguinte texto do idioma {sourceLanguage} para o idioma {targetLanguage}: \"{text}\"";

            prompt += "\n\nResponda apenas com o texto traduzido, sem nenhuma formatação ou explicação adicional.";

            var response = await _model.GenerateContentAsync(prompt, cancellationToken);
            return response.Text;
        }
    }
}
