using System.ComponentModel.DataAnnotations;

namespace StaticCRUD.Models
{
    public class EmployeeModel
    {
        public int employeeId { get; set; }


        [Required (ErrorMessage ="Employee First Name Should be Must")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Length should be 5-15")]
        [RegularExpression("^[a-b]{1,}$", ErrorMessage = "Only characters")]
        public string firstName { get; set; }



        [Required(ErrorMessage = "Employee Last Name Should be Must")]
        [StringLength(15, MinimumLength = 5)]
        [RegularExpression(@"^[a-b A-Z]$", ErrorMessage = "Only characters")]
        public string lastName { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]

        public string phoneNumber { get; set; }
        public string hireDate { get; set; }
        public string jobTitle { get; set; }
        public double salary { get; set; }
        public int departmentId { get; set; }

        [RegularExpression(@"^\d{8}$", ErrorMessage = "Password must be 8 characters or more")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password did not match")]
        public string ConfirmPassword { get; set; }
    }
}
