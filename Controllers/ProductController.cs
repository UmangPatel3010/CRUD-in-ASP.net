using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;
using System.Data.SqlClient;
using System.Data;

namespace StaticCRUD.Controllers
{
    public class ProductController : Controller
    {
        private IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public static List<ProductModel> products = new List<ProductModel>
        //{
        //    new ProductModel { ProductID = 1, ProductName = "Laptop", ProductPrice = 999.99m, ProductCode = "LP001", Description = "High-performance laptop", UserID = 101 },
        //    new ProductModel { ProductID = 2, ProductName = "Smartphone", ProductPrice = 499.99m, ProductCode = "SP002", Description = "Latest model smartphone", UserID = 102 },
        //    new ProductModel { ProductID = 3, ProductName = "Tablet", ProductPrice = 299.99m, ProductCode = "TB003", Description = "Lightweight and portable tablet", UserID = 103 }
        //};
        #region ProductList
        public IActionResult ProductList()
        {
            string connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetAllProducts";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return View(dt);
        }
        #endregion

        #region DropDown
        public void SetDropDown()
        {
            string connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "sp_UserDropDown";
            SqlDataReader reader1 = command.ExecuteReader();
            DataTable dataTable1 = new DataTable();
            dataTable1.Load(reader1);
            List<UserDropDownModel> userList = new List<UserDropDownModel>();
            foreach (DataRow user in dataTable1.Rows)
            {
                userList.Add(new UserDropDownModel() { UserID = Convert.ToInt32(user["UserID"]), UserName = user["UserName"].ToString() });
            }
            ViewBag.UserList = userList;
        }
        #endregion

        #region ProductForm
        public IActionResult ProductForm(int? ProductID)
        {
            SetDropDown();
            if (ProductID != null)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "sp_GetProductByID";
                command.Parameters.AddWithValue("ProductID", ProductID);
                SqlDataReader reader1 = command.ExecuteReader();
                DataTable dataTable1 = new DataTable();
                dataTable1.Load(reader1);
                ProductModel productModel = new ProductModel();
                productModel.ProductID = Convert.ToInt32(dataTable1.Rows[0]["ProductID"]);
                productModel.ProductName = dataTable1.Rows[0]["ProductName"].ToString();
                productModel.ProductPrice = Convert.ToDecimal(dataTable1.Rows[0]["ProductPrice"]);
                productModel.ProductCode = dataTable1.Rows[0]["ProductCode"].ToString();
                productModel.Description = dataTable1.Rows[0]["Description"].ToString();
                productModel.UserID = Convert.ToInt32(dataTable1.Rows[0]["UserID"]);
                connection.Close();
                return View(productModel);
            }
            return View();
        }
        #endregion

        #region [HttpPost] ProductForm
        [HttpPost]
        public IActionResult ProductForm(ProductModel pm)
        {
            ModelState.Remove("ProductID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (pm.ProductID != 0)
                {
                    command.CommandText = "sp_UpdateProduct";
                    command.Parameters.AddWithValue("ProductID", pm.ProductID);
                }
                else
                {
                    command.CommandText = "sp_InsertProduct";
                }
                command.Parameters.AddWithValue("ProductName", pm.ProductName);
                command.Parameters.AddWithValue("ProductPrice", pm.ProductPrice);
                command.Parameters.AddWithValue("ProductCode", pm.ProductCode);
                command.Parameters.AddWithValue("Description", pm.Description);
                command.Parameters.AddWithValue("UserID", pm.UserID);
                command.ExecuteNonQuery();
                connection.Close();
                ModelState.Clear();
                if (pm.ProductID != 0)
                    return RedirectToAction("ProductList");
                else
                {
                    TempData["SuccessMsg"] = $"<script>alert('{pm.ProductName} Product Added succesfully');</script>";
                    SetDropDown();
                }
            }
            return View();
        }
        #endregion

        #region Product Delete
        public IActionResult ProductDelete(int ProductID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_DeleteProduct";
                command.Parameters.AddWithValue("ProductID", ProductID);
                command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }
            return RedirectToAction("ProductList");
        }
        #endregion
    }
}
