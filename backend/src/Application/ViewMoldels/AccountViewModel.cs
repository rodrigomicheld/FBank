namespace Application.ViewMoldels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public int Number { get; set; }
        public AgencyViewModel Agency { get; set; }
        public decimal Balance { get; set; }
    }
}
