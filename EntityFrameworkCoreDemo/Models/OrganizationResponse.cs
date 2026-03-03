namespace EntityFrameworkCoreDemo.Models
{
    public class OrganizationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Address Address { get; set; }

        public List<EmployeeResponse> Employees { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
