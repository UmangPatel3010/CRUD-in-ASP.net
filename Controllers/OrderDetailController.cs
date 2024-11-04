using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;
using System.Data.SqlClient;
using System.Data;

namespace StaticCRUD.Controllers
{
    [CheckAccess]
    public class OrderDetailController : Controller
    {
        private IConfiguration _configuration;
        public OrderDetailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region OrderDetailList
        public IActionResult OrderDetailList()
        {
            String connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetAllOrderDetails";
            DataTable dt = new DataTable();
            SqlDataReader reader = command.ExecuteReader();
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
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_ProductDropDown";
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            List<ProductDropDownModel> productList = new List<ProductDropDownModel>();
            foreach (DataRow product in dt.Rows)
            {
                productList.Add(new ProductDropDownModel { ProductID = Convert.ToInt32(product["ProductID"]), ProductName = product["ProductName"].ToString() });
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
            command.CommandText = "sp_OrderDropDown";
            SqlDataReader reader2 = command.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<OrderDropDownModel> orderList = new List<OrderDropDownModel>();
            foreach (DataRow order in dataTable2.Rows)
            {
                orderList.Add(new OrderDropDownModel() { OrderID = Convert.ToInt32(order["OrderID"]), OrderNumber = order["OrderNumber"].ToString() });
            }
            ViewBag.OrderList = orderList;
            ViewBag.UserList = userList;
            ViewBag.ProductList = productList;
        }
        #endregion

        #region OrderDetailForm
        public IActionResult OrderDetailForm(int? OrderDetailID)
        {
            SetDropDown();
            if(OrderDetailID != null)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetOrderDetailByID";
                command.Parameters.AddWithValue("OrderDetailID", OrderDetailID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                OrderDetailModel orderDetail = new OrderDetailModel();
                orderDetail.OrderDetailID = Convert.ToInt32(dataTable.Rows[0]["OrderDetailID"]);
                orderDetail.OrderID = Convert.ToInt32(dataTable.Rows[0]["OrderID"]);
                orderDetail.ProductID = Convert.ToInt32(dataTable.Rows[0]["ProductID"]);
                orderDetail.Quantity = Convert.ToInt32(dataTable.Rows[0]["Quantity"]);
                orderDetail.Amount = Convert.ToDecimal(dataTable.Rows[0]["Amount"]);
                orderDetail.TotalAmount = Convert.ToDecimal(dataTable.Rows[0]["TotalAmount"]);
                orderDetail.UserID = Convert.ToInt32(dataTable.Rows[0]["UserID"]);
                connection.Close();
                return View(orderDetail);
            }
            return View();
        }
        #endregion

        #region [HttpPost] OrderDetailForm
        [HttpPost]
        public IActionResult OrderDetailForm(OrderDetailModel odm)
        {
            ModelState.Remove("OrderDetailID");
            ModelState.Remove("TotalAmount");
            if (ModelState.IsValid) {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (odm.OrderDetailID != 0)
                {
                    command.CommandText = "sp_UpdateOrderDetail";
                    command.Parameters.AddWithValue("OrderDetailID", odm.OrderDetailID);
                }
                else
                {
                    command.CommandText = "sp_InsertOrderDetail";
                }
                command.Parameters.AddWithValue("OrderID", odm.OrderID);
                command.Parameters.AddWithValue("ProductID", odm.ProductID);
                command.Parameters.AddWithValue("Quantity", odm.Quantity);
                command.Parameters.AddWithValue("Amount", odm.Amount);
                command.Parameters.AddWithValue("TotalAmount", odm.Quantity*odm.Amount);
                command.Parameters.AddWithValue("UserID", odm.UserID);
                command.ExecuteNonQuery();
                connection.Close();
                ModelState.Clear();
                if (odm.OrderDetailID != 0)
                    return RedirectToAction("OrderDetailList");
                else
                {
                    TempData["SuccessMsg"] = $"<script>alert('{odm.OrderID} OrderDetail Added succesfully');</script>";
                    SetDropDown();
                }
            }
            return View();
        }
        #endregion

        #region OrderDetailDelete
        public IActionResult OrderDetailDelete(int OrderDetailID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_DeleteOrderDetail";
                command.Parameters.AddWithValue("OrderDetailID", OrderDetailID);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }
            return RedirectToAction("OrderDetailList");
        }
        #endregion
    }
}
