using System.ComponentModel.DataAnnotations;
using StaticCRUD.Models;

namespace StaticCRUD.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product Price must be greater than 0.")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Product Code is required.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }
    }
    public class ProductDropDownModel
    {
        public int ProductID { get; set; }
        public String ProductName { get; set; }
    }
}
