namespace translate_app.Domain;

public class FileUploadResult
{
    public bool Success { get; set; }
    public string FilePath { get; set; }
    public string ErrorMessage { get; set; }
}