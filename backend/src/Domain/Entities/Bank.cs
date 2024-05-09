namespace Domain.Entities
{
    public class Bank : EntityBase
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Agency> Agencies { get; set; }
    }
}
