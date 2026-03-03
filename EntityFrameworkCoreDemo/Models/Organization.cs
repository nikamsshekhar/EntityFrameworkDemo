namespace EntityFrameworkCoreDemo.Models
{
    public class Organization
    {
        public string Name { get; set; }
        public int Phone { get; set; }
        public string PAN { get; set; }

        public int? AddressId { get; set; }
    }
}
