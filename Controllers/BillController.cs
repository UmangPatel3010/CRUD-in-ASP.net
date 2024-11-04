using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;
using System.Data.SqlClient;
using System.Data;

namespace StaticCRUD.Controllers
{
    [CheckAccess]
    public class BillController : Controller
    {
        private IConfiguration _configuration;
        public BillController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //List<BillModel> bills = new List<BillModel>
        //{
        //    new BillModel { BillID = 1, BillNumber = "BILL001", BillDate = DateTime.Now.AddDays(-1), OrderID = 1, TotalAmount = 200.00m, Discount = 10.00m, NetAmount = 190.00m, UserID = 1 },
        //    new BillModel { BillID = 3, BillNumber = "BILL003", BillDate = DateTime.Now.AddDays(-2), OrderID = 3, TotalAmount = 300.00m, Discount = 20.00m, NetAmount = 280.00m, UserID = 3 },
        //    new BillModel { BillID = 2, BillNumber = "BILL002", BillDate = DateTime.Now, OrderID = 2, TotalAmount = 150.00m, Discount = null, NetAmount = 150.00m, UserID = 2 },
        //};

        #region BillList
        public IActionResult BillList()
        {
            String connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetAllBills";
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
        }
        #endregion

        #region BillForm
        public IActionResult BillForm(int? BillID)
        {
            SetDropDown();
            if (BillID != null)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "sp_GetBillByID";
                command.Parameters.AddWithValue("BillID", BillID);
                SqlDataReader reader1 = command.ExecuteReader();
                DataTable dataTable1 = new DataTable();
                dataTable1.Load(reader1);
                BillModel billModel = new BillModel();
                billModel.BillID = Convert.ToInt32(dataTable1.Rows[0]["BillID"]);
                billModel.BillNumber = dataTable1.Rows[0]["BillNumber"].ToString();
                billModel.BillDate = Convert.ToDateTime(dataTable1.Rows[0]["BillDate"]);
                billModel.OrderID = Convert.ToInt32(dataTable1.Rows[0]["OrderID"]);
                billModel.TotalAmount = Convert.ToDecimal(dataTable1.Rows[0]["TotalAmount"]);
                billModel.Discount = Convert.ToDecimal(dataTable1.Rows[0]["Discount"]);
                billModel.NetAmount = Convert.ToDecimal(dataTable1.Rows[0]["NetAmount"]);
                billModel.UserID = Convert.ToInt32(dataTable1.Rows[0]["UserID"]);
                connection.Close();
                return View(billModel);
            }
            return View();
        }
        #endregion

        #region [HttpPost] BillForm
        [HttpPost]
        public IActionResult BillForm(BillModel bm)
        {
            ModelState.Remove("BillID");
            ModelState.Remove("NetAmount");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (bm.BillID != 0)
                {
                    command.CommandText = "sp_UpdateBill";
                    command.Parameters.AddWithValue("BillID", bm.BillID);
                }
                else
                {
                    command.CommandText = "sp_InsertBill";
                }
                command.Parameters.AddWithValue("BillNumber", bm.BillNumber);
                command.Parameters.AddWithValue("BillDate", bm.BillDate);
                command.Parameters.AddWithValue("OrderID", bm.OrderID);
                command.Parameters.AddWithValue("TotalAmount", bm.TotalAmount);
                command.Parameters.AddWithValue("Discount", bm.Discount);
                command.Parameters.AddWithValue("NetAmount", bm.TotalAmount-bm.Discount);
                command.Parameters.AddWithValue("UserID", bm.UserID);
                command.ExecuteNonQuery();
                connection.Close();
                ModelState.Clear();
                if (bm.BillID != 0)
                    return RedirectToAction("BillList");
                else
                {
                    TempData["SuccessMsg"] = $"<script>alert('{bm.BillNumber} Bill Added succesfully');</script>";
                    SetDropDown();
                }
            }
            return View();
        }
        #endregion

        #region Bill Delete
        public IActionResult BillDelete(int BillID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_DeleteBill";
                command.Parameters.AddWithValue("BillID", BillID);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }
            return RedirectToAction("BillList");
        }
        #endregion
    }
}
