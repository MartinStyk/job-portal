namespace Infrastructure.Entity
{
    public interface IEntity
    {
        int Id { get; set; }

        string TableName { get; }
    }
}