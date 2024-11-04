using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;
using System.Data.SqlClient;
using System.Data;
namespace StaticCRUD.Controllers
{
    [CheckAccess]
    public class CustomerController : Controller
    {
        private IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region CustomerList
        public IActionResult CustomerList()
        {
            String connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetAllCustomers";
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

        #region CustomerForm
        public IActionResult CustomerForm(int? CustomerID)
        {
            SetDropDown();
            if (CustomerID != null)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "sp_GetCustomerByID";
                command.Parameters.AddWithValue("CustomerID", CustomerID);
                SqlDataReader reader1 = command.ExecuteReader();
                DataTable dataTable1 = new DataTable();
                dataTable1.Load(reader1);
                CustomerModel customerModel = new CustomerModel();
                customerModel.CustomerID = Convert.ToInt32(dataTable1.Rows[0]["CustomerID"]);
                customerModel.CustomerName = dataTable1.Rows[0]["CustomerName"].ToString();
                customerModel.HomeAddress = dataTable1.Rows[0]["HomeAddress"].ToString();
                customerModel.Email = dataTable1.Rows[0]["Email"].ToString();
                customerModel.MobileNo = dataTable1.Rows[0]["MobileNo"].ToString();
                customerModel.GSTNO = dataTable1.Rows[0]["GSTNO"].ToString().Substring(3);
                customerModel.CityName = dataTable1.Rows[0]["CityName"].ToString();
                customerModel.PinCode = dataTable1.Rows[0]["PinCode"].ToString();
                customerModel.NetAmount = Convert.ToInt32(dataTable1.Rows[0]["NetAmount"]);
                customerModel.UserID = Convert.ToInt32(dataTable1.Rows[0]["UserID"]);
                connection.Close();
                return View(customerModel);
            }
            return View();
        }
        #endregion

        #region [HttpPost] CustomerForm
        [HttpPost]
        public IActionResult CustomerForm(CustomerModel cm)
        {
            ModelState.Remove("CustomerID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (cm.CustomerID != 0)
                {
                    command.CommandText = "sp_UpdateCustomer";
                    command.Parameters.AddWithValue("CustomerID", cm.CustomerID);
                }
                else
                {
                    command.CommandText = "sp_InsertCustomer";
                }
                command.Parameters.AddWithValue("CustomerName", cm.CustomerName);
                command.Parameters.AddWithValue("HomeAddress", cm.HomeAddress);
                command.Parameters.AddWithValue("Email", cm.Email);
                command.Parameters.AddWithValue("MobileNo", cm.MobileNo);
                command.Parameters.AddWithValue("GSTNO", "GST"+cm.GSTNO);
                command.Parameters.AddWithValue("CityName", cm.CityName);
                command.Parameters.AddWithValue("PinCode", cm.PinCode);
                command.Parameters.AddWithValue("NetAmount", cm.NetAmount);
                command.Parameters.AddWithValue("UserID", cm.UserID);
                command.ExecuteNonQuery();
                connection.Close();
                ModelState.Clear();
                if (cm.CustomerID != 0)
                    return RedirectToAction("CustomerList");
                else
                {
                    TempData["SuccessMsg"] = $"<script>alert('{cm.CustomerName} Customer Added succesfully');</script>";
                    SetDropDown();
                }
            }
            return View();
        }
        #endregion

        #region CustomerDelete
        public IActionResult CustomerDelete(int CustomerID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_DeleteCustomer";
                command.Parameters.AddWithValue("CustomerID", CustomerID);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }
            return RedirectToAction("CustomerList");
        }
        #endregion
    }
}
