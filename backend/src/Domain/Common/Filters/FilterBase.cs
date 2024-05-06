namespace Domain.Common.Filters
{
    public class FilterBase
    {
        public int _page { get; set; } = 0;
        public int _size { get; set; } = 10;
        public string _order { get; set;}
    }
}
