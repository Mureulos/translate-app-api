using MediatR;
using translate_app.Application.UseCases.Languages.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.Languages.GetAllLanguages
{
    public sealed class Handler: IRequestHandler<GetAllLanguagesQuery, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly AIService _aiService;

        public Handler(ILanguageRepository languageRepository, AIService aiService)
        {
            _languageRepository = languageRepository;
            _aiService = aiService;
        }

        public async Task<Result<Response>> Handle(GetAllLanguagesQuery query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var allLanguages = await _languageRepository.GetAllLanguages(cancellationToken);

            if (allLanguages is null || !allLanguages.Any())
                return Result.Success(new Response(Array.Empty<LanguageResponse>()));

            var responseList = allLanguages.Select(item => new LanguageResponse
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name ?? string.Empty,
                LocalizedName = item.Name ?? string.Empty
            }).ToArray();

            return Result.Success(new Response(responseList));
        }
    }
}
