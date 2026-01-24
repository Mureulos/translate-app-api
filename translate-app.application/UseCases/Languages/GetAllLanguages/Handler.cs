using System;
using System.Linq;
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
                return Result.Failure<Response>(new Error("404", "Languages not found"));


            var translationTasks = allLanguages.Select(async item =>
            {
                var localizedName = item.Name ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(item.Name))
                {
                    try
                    {
                        var aiTranslation = await _aiService.TranslateAsync(item.Name, "English", item.Name, cancellationToken);

                        if (!string.IsNullOrWhiteSpace(aiTranslation))
                            localizedName = aiTranslation.Trim();
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                    }
                }

                return new LanguageResponse
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    LocalizedName = localizedName
                };
            });

            var translatedLanguages = (await Task.WhenAll(translationTasks)).ToArray();

            return Result.Success(new Response(translatedLanguages));
        }
    }
}
