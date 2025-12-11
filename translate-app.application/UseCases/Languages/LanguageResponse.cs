namespace translate_app.Application.UseCases.Languages
{
    public class LanguageResponse
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LocalizedName { get; set; } = string.Empty;
    }
}