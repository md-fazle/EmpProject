using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models.Entities
{
    public class Employee
    {
        [Key]
        public Guid id { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }
        public string  Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }

        public string Department { get; set; }

        public string Salary { get; set; }

        public string Address { get; set; } 


    }
}
