/*
    Command é um objeto imutável de entrada que carrega os dados necessários para executar um caso de uso.
*/

using MediatR;
using translate_app.Application.UseCases.Translation.Entities.DTOs;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed record TranslateCommand(TranslationDto dto) : IRequest<Result<Response>>;
}
