/*
    Response é o objeto de saída, retornado após o processamento.
*/

using translate_app.Application.UseCases.User.Entities.Response;

namespace translate_app.Application.UseCases.User.GetAllUsers
{
    public sealed record Response(UserResponse[] response);
}
