using SampleProject.Model;

namespace SampleProject.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int DeptId { get; set; } // F-Key
        

        public Department? Dept { get; set; } // Department table
    }
}
