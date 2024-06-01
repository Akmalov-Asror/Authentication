using Teaching.Common.Entities.Users;

namespace Teaching.V1.Auth.Services.Interfaces;

public interface ITokenRepository
{
    string CreateToken(User user, IList<string> roles);
}
