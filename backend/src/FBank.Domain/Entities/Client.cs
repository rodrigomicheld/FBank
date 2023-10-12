using FBank.Domain.Enums;

namespace FBank.Domain.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public PersonType DocumentType { get; set; }
        public virtual IEnumerable<Account> Accounts { get; set; }
        
    }
}
