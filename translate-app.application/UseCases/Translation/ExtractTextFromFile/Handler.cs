using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Services;

namespace translate_app.application.UseCases.Translation.TranslateFile
{
    public sealed class Handler: IRequestHandler<ExtractTextFromFileCommand, Result<Response>>
    {
        private readonly IExtractTextService _extractTextFromFileService;

        public Handler(IExtractTextService extractTextFromFileService)
        {
            _extractTextFromFileService = extractTextFromFileService;
        }

        public async Task<Result<Response>> Handle(ExtractTextFromFileCommand command, CancellationToken cancellationToken)
        {
            var uploadFileResult = await _extractTextFromFileService.ExtractFileAsync(command.File);
            
            if (!uploadFileResult.Success)
                return Result.Failure<Response>(new Error("Upload.Failed", uploadFileResult.ErrorMessage));
            
            return Result.Success(new Response(uploadFileResult));
        }
    }
}

