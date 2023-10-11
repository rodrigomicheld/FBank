using FBank.Domain.Entities;
using FBank.Domain.Enums;

namespace FBank.Application.ViewMoldels
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DocumentType DocumentType { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
