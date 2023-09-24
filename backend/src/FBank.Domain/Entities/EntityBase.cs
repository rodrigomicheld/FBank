namespace FBank.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreateDateAt { get; set; }
        public DateTime UpdateDateAt { get; set; }
   
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}