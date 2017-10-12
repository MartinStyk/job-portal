namespace Infrastructure.Entity
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }

        string TableName { get; }
    }
}