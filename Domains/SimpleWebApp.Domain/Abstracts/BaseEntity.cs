namespace SimpleWebApp.Domain.Abstracts
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected internal set; }

        public BaseEntity() { }

        public BaseEntity(Guid id) => Id = id;
    }
}
