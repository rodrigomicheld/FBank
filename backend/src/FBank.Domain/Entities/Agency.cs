namespace FBank.Domain.Entities
{
    public class Agency  : EntityBase
    {
        public int Code { get; set; }
        public int BankCode { get; set; }
        public string Name { get; set; }        
    }
}
