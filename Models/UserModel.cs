using System.ComponentModel.DataAnnotations;
using StaticCRUD.Models;

namespace StaticCRUD.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain both letters and numbers.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile No. is required.")]
        [Phone(ErrorMessage = "Invalid Mobile No.")]
        [RegularExpression("^[1-9]{1}[0-9]{9}$",ErrorMessage ="It should have 10 digits only")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Is Active status is required.")]
        public bool IsActive { get; set; }
    }
    public class UserDropDownModel
    {
        public int UserID { get; set; }
        public String UserName { get; set; }
    }
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage ="Enter a Valid Email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public required string Password { get; set; }
    }
}