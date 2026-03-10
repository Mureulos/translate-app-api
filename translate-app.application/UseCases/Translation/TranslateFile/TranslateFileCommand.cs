using MediatR;
using Microsoft.AspNetCore.Http;
using translate_app.Domain.Abstractions;

namespace translate_app.application.UseCases.Translation.TranslateFile;

public sealed record TranslateFileCommand(IFormFile File): IRequest<Result<Response>>;