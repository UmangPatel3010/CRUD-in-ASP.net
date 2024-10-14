using System.ComponentModel.DataAnnotations;
using StaticCRUD.Models;

namespace StaticCRUD.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Order Number is required.")]
        [RegularExpression("^\\d{5}$",ErrorMessage ="Invalid Order Number (Enter 5-Digits)")]
        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerID { get; set; }

        public string PaymentMode { get; set; }

        [Required(ErrorMessage = "Total Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total Amount must be greater than 0.")]
        public decimal? TotalAmount { get; set; }

        [Required(ErrorMessage = "Shipping Address is required.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }
    }
    public class OrderDropDownModel
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
    }
}
