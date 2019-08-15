using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Employee
    {
        public Employee()
        {
            Department = new Department();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public Nullable<int> DepartmentId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(20, ErrorMessage = "Max lenght is 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Nullable<byte> Gender { get; set; }

        [Required(ErrorMessage = "Adress is required")]
        [MaxLength(50, ErrorMessage = "Max lenght is 20 characters")]
        public string Address { get; set; }

        public Department Department { get; set; }
    }
}
