using Microsoft.Extensions.Configuration;
using translate_app.Domain.Entities;

namespace translate_app.Domain.Services;

public interface ITokenService
{
    public string Create(User user);
}
