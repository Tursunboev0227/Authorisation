using Authorication.Domain.Entities.ForToken;

namespace Authorisation.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<string> GenerateToken(string login, string password);
    }
}