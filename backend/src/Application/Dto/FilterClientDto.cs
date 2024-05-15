using Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Common.Filters
{
    [ExcludeFromCodeCoverage]
    public class FilterClientDto : FilterBase
    {
        public DateTime InitialDate { get; set; } = DateTime.MinValue;
        public DateTime FinalDate { get; set; } = DateTime.Now;
        public FlowType? FlowType { get; set;}
    }
}
