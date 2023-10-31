using FBank.Domain.Enums;

namespace FBank.Domain.Common.Filters
{
    public class FilterClientDto : FilterBase
    {
        public DateTime InitialDate { get; set; } = DateTime.MinValue;
        public DateTime FinalDate { get; set; } = DateTime.Now;
        public FlowType? FlowType { get; set;}
    }
}
