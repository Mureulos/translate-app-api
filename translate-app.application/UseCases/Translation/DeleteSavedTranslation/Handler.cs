using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.application.UseCases.Translation.DeleteSavedTranslation;

public sealed class Handler: IRequestHandler<DeleteSavedTranslationCommand, Result<Response>> 
{

    private readonly ITranslationRepository _translationRepository;

    public Handler(ITranslationRepository translationRepository)
    {
        _translationRepository = translationRepository;
    }

    public async Task<Result<Response>> Handle(DeleteSavedTranslationCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        if (command.savedTranslationId <= 0)
            return Result.Failure<Response>(new Error("Translation.NotFound", "Translation not found", ErrorType.NotFound));

        await _translationRepository.DeleteSavedTranslations(command.savedTranslationId, cancellationToken);
        
        return Result.Success(new Response());
    }
}