namespace FBank.Domain.Entities
{
    public class Agency  : EntityBase
    {
        public int BankCode { get; set; }
        public string Name { get; set; }        
        public Bank Bank { get; set; }
    }
}
