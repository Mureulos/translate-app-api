using translate_app.Domain.Entities.Response;

namespace translate_app.application.UseCases.Translation.GetSavedTranslations;

public sealed record Response(SavedTranslationResponse[] response);
