namespace ATON_ITTP_2024_Domain.Entity
{
    public abstract class Entity<TPrimaryKey>
    {
        public TPrimaryKey? Id { get; set; }
    }

    public abstract class Entity : Entity<int> { }
}
