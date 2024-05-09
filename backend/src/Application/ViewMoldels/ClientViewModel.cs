namespace Application.ViewMoldels
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string PersonType { get; set; }
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}
