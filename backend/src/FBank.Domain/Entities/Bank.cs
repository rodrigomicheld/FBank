namespace FBank.Domain.Entities
{
    public class Bank : EntityBase
    {        
        public string Name { get; set; }
        public virtual IEnumerable<Agency> Agencies { get; set; }
    }
}
