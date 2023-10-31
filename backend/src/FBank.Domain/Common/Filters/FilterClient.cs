using FBank.Domain.Enums;

namespace FBank.Domain.Common.Filters
{
    public class FilterClient : FilterBase
    {
        public DateTime InitialDate { get; set; } = DateTime.MinValue;
        public DateTime FinalDate { get; set; } = DateTime.Now;
        public int NumberAgency { get; set;}
        public int NumberAccount { get; set;}
        public FlowType? FlowType { get; set;}
    }
}
