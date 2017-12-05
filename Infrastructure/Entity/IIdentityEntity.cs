using Microsoft.Build.Framework;

namespace Infrastructure.Entity
{
    public interface IIdentityEntity : IEntity
    {        
        string Email { get; set; }

        string PasswordSalt { get; set; }

        string PasswordHash { get; set; }

        /// <summary>
        /// String with , delimiter.
        /// For example: "Admin,Editor,Tutor"
        /// </summary>
        string Roles { get; set; }

    }
}