using MediatR;
using translate_app.Application.UseCases.Languages.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.Languages.GetLanguageById
{
    public sealed class Handler: IRequestHandler<GetLanguageByIdQuery, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly AIService _aiService;

        public Handler(ILanguageRepository languageRepository, AIService aiService)
        {
            _languageRepository = languageRepository;
            _aiService = aiService;
        }

        public async Task<Result<Response>> Handle(GetLanguageByIdQuery query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = await _languageRepository.GetLanguageById(query.idLanguage, cancellationToken);
            var localizedName = await _aiService.TranslateAsync(response.Name, "English", response.Name);

            var language = new LanguageResponse
            {
                Id = query.idLanguage,
                Code = response.Code,
                Name = response.Name,
                LocalizedName = localizedName,
            };
            
            return Result.Success(new Response(language));
        }
    }
}
