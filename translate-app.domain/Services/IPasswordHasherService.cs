namespace translate_app.Domain.Services;

public interface IPasswordHasherService
{
    public string Hash(string password);
    public bool Verify(string password, string hashedPassword);
}