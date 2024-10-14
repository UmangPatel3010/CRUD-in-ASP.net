using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;
using System.Data.SqlClient;
using System.Data;

namespace StaticCRUD.Controllers
{
    public class UserController : Controller
    {

        private IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region UserList
        public IActionResult UserList()
        {
            String connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetAllUsers";
            DataTable dt = new DataTable();
            SqlDataReader sdr = command.ExecuteReader();
            dt.Load(sdr);
            return View(dt);
        }
        #endregion

        #region UserForm
        public IActionResult UserForm(int? UserID)
        {
            if (UserID != null)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "sp_GetUserByID";
                command.Parameters.AddWithValue("UserID", UserID);
                SqlDataReader reader1 = command.ExecuteReader();
                DataTable dataTable1 = new DataTable();
                dataTable1.Load(reader1);
                UserModel userModel = new UserModel();
                userModel.UserID = Convert.ToInt32(dataTable1.Rows[0]["UserID"]);
                userModel.UserName = dataTable1.Rows[0]["UserName"].ToString();
                userModel.Email = dataTable1.Rows[0]["Email"].ToString();
                userModel.Password = dataTable1.Rows[0]["Password"].ToString();
                userModel.MobileNo = dataTable1.Rows[0]["MobileNo"].ToString();
                userModel.Address = dataTable1.Rows[0]["Address"].ToString();
                userModel.IsActive = Convert.ToBoolean(dataTable1.Rows[0]["IsActive"]);
                connection.Close();
                return View(userModel);
            }
            return View();
        }
        #endregion

        #region [HttpPost] UserForm
        [HttpPost]
        public IActionResult UserForm(UserModel um)
        {
            ModelState.Remove("UserID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (um.UserID != 0)
                {
                    command.CommandText = "sp_UpdateUser";
                    command.Parameters.AddWithValue("UserID", um.UserID);
                }
                else
                {
                    command.CommandText = "sp_InsertUser";
                }
                command.Parameters.AddWithValue("UserName", um.UserName);
                command.Parameters.AddWithValue("Email", um.Email);
                command.Parameters.AddWithValue("Password", um.Password);
                command.Parameters.AddWithValue("MobileNo", um.MobileNo);
                command.Parameters.AddWithValue("Address", um.Address);
                command.Parameters.AddWithValue("IsActive", um.IsActive);
                command.ExecuteNonQuery();
                connection.Close();
                ModelState.Clear();
                if (um.UserID != 0)
                    return RedirectToAction("UserList");
                else
                    TempData["SuccessMsg"] = $"<script>alert('{um.UserName} User Added succesfully');</script>";
            }
            return View();
        }
        #endregion

        #region UserDelete
        public IActionResult UserDelete(int UserID)
        {
            try
            {

                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_DeleteUser";
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }
            return RedirectToAction("UserList");
        }
        #endregion

        #region UserLogin

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(UserLoginModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    String connectionString = this._configuration.GetConnectionString("myConnectionString");
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "sp_LoginUser";
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    SqlDataReader reader1 = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader1);
                    if (dt.Rows.Count > 0)
                    {
                        HttpContext.Session.SetString("Email", dt.Rows[0]["Email"].ToString());
                        HttpContext.Session.SetString("Password", dt.Rows[0]["Password"].ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid Username or Password";
                        //return RedirectToAction("UserLogin", "User");
                    }
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }
            return RedirectToAction("Login");
        }
        #endregion

        #region UserRegister
        [HttpGet]
        public ActionResult UserRegister()
        {
            return View();
        }
        #endregion
    }
}
