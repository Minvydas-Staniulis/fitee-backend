using fitee_backend.Model;

namespace fitee_backend.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);  
    }
}
