namespace translate_app.application.UseCases.Translation.SaveTranslation;

public sealed record Response(int Id, string Text, string TranslationText, int UserId, DateTime CreatedAt);
