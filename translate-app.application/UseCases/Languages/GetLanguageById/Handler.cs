using MediatR;
using System.Threading;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.Languages.GetLanguageById
{
    public sealed class Handler: IRequestHandler<Command, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;

        public Handler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<Result<Response>> Handle(Command command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = await _languageRepository.GetLanguageById(command.idLanguage, cancellationToken);

            var language = new Language
            {
                Code = response.Code,
                Name = response.Name,
                LocalizedName = response.LocalizedName,
            };
            
            return Result.Success(new Response(language));
        }
    }
}
