using FBank.Domain.Enums;

namespace FBank.Domain.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public string Document { get => document; set => document = FormatDocument(value); }
        public string Password { get; set; }
        public PersonType DocumentType { get ; set ; }
        public virtual IEnumerable<Account> Accounts { get; set; }

        private string document; 
        private string FormatDocument(string value)
        {
            if (value != null)
            {
                return value.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            }
            return null;
        }
    }
}
