using FBank.Domain.Enums;

namespace FBank.Domain.Common.Filters
{
    public class FilterClient : FilterBase
    {
        public DateTime InitialDate { get; set; } = DateTime.Now;
        public DateTime FinalDate { get; set; } = DateTime.Now;
        public int ClientAgency { get; set;}
        public int ClientAccount { get; set;}
        public FlowType? FlowType { get; set;}
    }
}
