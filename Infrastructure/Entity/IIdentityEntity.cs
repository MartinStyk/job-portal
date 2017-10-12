namespace Infrastructure.Entity
{
    public interface IIdentityEntity<T> : IEntity<T>
    {        
        string Email { get; set; }

        string Password { get; set; }

    }
}