using MediatR;
using Microsoft.AspNetCore.Mvc;
using translate_app.Domain;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Services;
using translate_app.Infrastructure.Services;

namespace translate_app.application.UseCases.Translation.TranslateFile
{
    public sealed class Handler: IRequestHandler<TranslateFileCommand, Result<Response>>
    {
        private readonly IFileUploadService _fileUploadService;

        public Handler(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public async Task<Result<Response>> Handle(TranslateFileCommand command, CancellationToken cancellationToken)
        {
            var result = await _fileUploadService.UploadFileAsync(command.File);
            
            if (!result.Success)
            {
                return Result.Failure<Response>(new Error("Upload.Failed", result.ErrorMessage));
            }

            return Result.Success(new Response(result.FilePath));
        }
    }
}

