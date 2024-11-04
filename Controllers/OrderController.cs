using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;
using System.Data;
using System.Data.SqlClient;

namespace StaticCRUD.Controllers
{
    [CheckAccess]
    public class OrderController : Controller
    {
        private IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //List<OrderModel> orders = new List<OrderModel>
        //{
        //    new OrderModel { OrderID = 1, OrderDate = DateTime.Now.AddDays(-1), CustomerID = 1001, PaymentMode = "Credit Card", TotalAmount = 150.75m, ShippingAddress = "123 Elm St", UserID = 1 },
        //    new OrderModel { OrderID = 2, OrderDate = DateTime.Now, CustomerID = 1002, PaymentMode = null, TotalAmount = null, ShippingAddress = "456 Oak St", UserID = 2 },
        //    new OrderModel { OrderID = 3, OrderDate = DateTime.Now.AddDays(-2), CustomerID = 1003, PaymentMode = "PayPal", TotalAmount = 75.50m, ShippingAddress = "789 Pine St", UserID = 3 }
        //};

        #region OrderList
        public IActionResult OrderList()
        {
            string connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "sp_GetAllOrders";
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
            command.CommandText = "sp_CustomerDropDown";
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            List<CustomerDropDownModel> customerList = new List<CustomerDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                customerList.Add(new CustomerDropDownModel { CustomerID = Convert.ToInt32(dr["CustomerID"]), CustomerName = dr["CustomerName"].ToString() });
            }
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
            ViewBag.CustomerList = customerList;
        }
        #endregion

        #region OrderForm
        public IActionResult OrderForm(int? OrderID)
        {
            SetDropDown();
            if (OrderID != null)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "sp_GetOrderByID";
                command.Parameters.AddWithValue("OrderID", OrderID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                OrderModel orderModel = new OrderModel();
                orderModel.OrderID = Convert.ToInt32(dataTable.Rows[0]["OrderID"]);
                orderModel.OrderNumber = dataTable.Rows[0]["OrderNumber"].ToString().Substring(3);
                orderModel.OrderDate = Convert.ToDateTime(dataTable.Rows[0]["OrderDate"]);
                orderModel.CustomerID = Convert.ToInt32(dataTable.Rows[0]["CustomerID"]);
                orderModel.PaymentMode = dataTable.Rows[0]["PaymentMode"].ToString();
                orderModel.TotalAmount = Convert.ToInt32(dataTable.Rows[0]["TotalAmount"]);
                orderModel.ShippingAddress = dataTable.Rows[0]["ShippingAddress"].ToString();
                orderModel.UserID = Convert.ToInt32(dataTable.Rows[0]["UserID"]);
                connection.Close();
                return View(orderModel);
            }
            return View();
        }
        #endregion

        #region [HttpPost] CustomerForm
        [HttpPost]
        public IActionResult OrderForm(OrderModel cm)
        {
            ModelState.Remove("OrderID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (cm.OrderID != 0)
                {
                    command.CommandText = "sp_UpdateOrder";
                    command.Parameters.AddWithValue("OrderID", cm.OrderID);
                }
                else
                {
                    command.CommandText = "sp_InsertOrder";
                }
                command.Parameters.AddWithValue("OrderNumber", "ORN"+cm.OrderNumber);
                command.Parameters.AddWithValue("OrderDate", cm.OrderDate);
                command.Parameters.AddWithValue("CustomerID", cm.CustomerID);
                command.Parameters.AddWithValue("PaymentMode", cm.PaymentMode);
                command.Parameters.AddWithValue("TotalAmount", cm.TotalAmount);
                command.Parameters.AddWithValue("ShippingAddress", cm.ShippingAddress);
                command.Parameters.AddWithValue("UserID", cm.UserID);
                command.ExecuteNonQuery();
                connection.Close();
                ModelState.Clear();
                if (cm.OrderID != 0)
                    return RedirectToAction("OrderList");
                else
                {
                    TempData["SuccessMsg"] = $"<script>alert('{cm.OrderNumber} Order Added succesfully');</script>";
                    SetDropDown();
                }
            }
            return View();
        }
        #endregion

        #region OrderDelete
        public IActionResult OrderDelete(int OrderID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_DeleteOrder";
                command.Parameters.AddWithValue("OrderID", OrderID);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }
            return RedirectToAction("OrderList");   
        }
        #endregion
    }
}
