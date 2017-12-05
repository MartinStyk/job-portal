using System;

namespace BusinessLayer.Services.Auth
{
    public interface IAuthenticationService
    {
        bool VerifyHashedPassword(string hashedPassword, string salt, string password);

        Tuple<string, string> CreateHash(string password);
    }
}