using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.Response;
using translate_app.Domain.Repositories;

namespace translate_app.application.UseCases.Translation.GetSavedTranslations;

public sealed class Handler : IRequestHandler<GetSavedTranslationsQuery, Result<Response>>
{
    private readonly ITranslationRepository _translationRepository;

    public Handler(ITranslationRepository translationRepository)
    {
        _translationRepository = translationRepository;
    }

    public async Task<Result<Response>> Handle(GetSavedTranslationsQuery query, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var allSaves = await _translationRepository.GetSavedTranslations(query.userId, cancellationToken);
        
        if (allSaves is null || !allSaves.Any())
            return Result.Failure<Response>(new Error("404", "Translations not saved yet"));

        var savedTranslations = allSaves.Select(item => new SavedTranslationResponse 
        {
            Id = item.Id,
            Text = item.Text,
            TranslationText = item.TranslationText,
            UserId = item.UserId,
            CreatedAt = item.CreatedAt
        }).ToArray();
        
        return Result.Success(new Response(savedTranslations));
    }
}