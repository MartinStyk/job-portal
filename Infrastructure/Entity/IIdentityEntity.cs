namespace Infrastructure.Entity
{
    public interface IIdentityEntity : IEntity
    {        
        string Email { get; set; }

        string Password { get; set; }

    }
}