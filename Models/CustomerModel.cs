using System.ComponentModel.DataAnnotations;
using StaticCRUD.Models;

namespace StaticCRUD.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Home Address is required.")]
        public string HomeAddress { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile No is required.")]
        [Phone(ErrorMessage = "Invalid Mobile No.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "GST NO is required.")]
        [RegularExpression("^\\d{6}$", ErrorMessage = "Invalid GST Number (Enter 6-Digit)")]
        public string GSTNO { get; set; }

        [Required(ErrorMessage = "City Name is required.")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Pin Code is required.")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "Net Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Net Amount must be greater than 0.")]
        public decimal NetAmount { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }
    }
    public class CustomerDropDownModel
    {
        public int CustomerID { get; set; }
        public String CustomerName { get; set; }
    }
}
