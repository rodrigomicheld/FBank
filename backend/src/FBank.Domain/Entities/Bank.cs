namespace FBank.Domain.Entities
{
    public class Bank : EntityBase
    {        
        public string Name { get; set; }
        public IEnumerable<Agency> Agencies { get; set; }
    }
}
