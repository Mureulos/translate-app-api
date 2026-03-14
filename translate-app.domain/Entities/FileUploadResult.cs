namespace translate_app.Domain;

public class FileUploadResult
{
    public bool Success { get; set; }
    public string FilePath { get; set; } = String.Empty;
    public string ExtractedContent { get; set; } = String.Empty;
    public string ErrorMessage { get; set; } = String.Empty;
}