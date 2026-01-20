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

            // Cria uma lista de "tarefas de tradução" (tasks)
            var translationTasks = allLanguages.Select(async item =>
            {
                var localizedName = await _aiService.TranslateAsync(item.Name, "English", item.Name);
                return new LanguageResponse
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    LocalizedName = localizedName
                };
            });

            // Aguarda TODAS as tarefas de tradução terminarem em paralelo
            var translatedLanguages = await Task.WhenAll(translationTasks);

            return Result.Success(new Response(translatedLanguages));
        }
    }
}
