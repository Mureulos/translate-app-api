using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.Languages.GetAllLanguages
{
    public sealed class Handler: IRequestHandler<Command, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;

        public Handler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var allLanguages = await _languageRepository.GetAllLanguages(cancellationToken);
            return Result.Success(new Response(allLanguages.ToArray()));
        }
    }
}
