using MediatR;
using System.Threading;
using translate_app.Application.UseCases.Languages.GetAllLanguages;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.Languages.GetLanguageById
{
    public sealed class Handler: IRequestHandler<Command, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly AIService _aiService;

        public Handler(ILanguageRepository languageRepository, AIService aiService)
        {
            _languageRepository = languageRepository;
            _aiService = aiService;
        }

        public async Task<Result<Response>> Handle(Command command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = await _languageRepository.GetLanguageById(command.idLanguage, cancellationToken);
            var localizedName = await _aiService.TranslateAsync(response.Name, "English", response.Name);

            var language = new LanguageResponse
            {
                Id = command.idLanguage,
                Code = response.Code,
                Name = response.Name,
                LocalizedName = localizedName,
            };
            
            return Result.Success(new Response(language));
        }
    }
}
