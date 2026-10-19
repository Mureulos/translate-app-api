using translate_app.Application.UseCases.TranslationMemory.Models;

namespace translate_app.Application.UseCases.TranslationMemory.SearchTranslationMemory;

public sealed record Response(TranslationMemoryItemResponse[] Items);
