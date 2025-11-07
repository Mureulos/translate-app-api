namespace translate_app.Application.UseCases.Languages
{
    public class LanguageResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string LocalizedName { get; set; }
    }
}