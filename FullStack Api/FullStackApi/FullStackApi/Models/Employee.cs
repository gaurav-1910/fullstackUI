namespace FullStackApi.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long number { get; set; }
        public long salary { get; set;}
        public string Department { get; set;}
    }
}
