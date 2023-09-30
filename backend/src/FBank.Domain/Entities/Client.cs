using FBank.Domain.Enums;

namespace FBank.Domain.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
