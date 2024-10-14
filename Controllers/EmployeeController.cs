using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;

namespace StaticCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        List<EmployeeModel> employees = new List<EmployeeModel>
        {
            new EmployeeModel { employeeId = 1, firstName = "John", lastName = "Doe", email = "john.doe@example.com", phoneNumber = "123-456-7890", hireDate = "2021-01-15", jobTitle = "Software Engineer", salary = 60000.00, departmentId = 5 },
            new EmployeeModel { employeeId = 2, firstName = "Jane", lastName = "Smith", email = "jane.smith@example.com", phoneNumber = "234-567-8901", hireDate = "2020-03-22", jobTitle = "Financial Analyst", salary = 55000.00, departmentId = 2 },
            new EmployeeModel { employeeId = 3, firstName = "Emily", lastName = "Johnson", email = "emily.johnson@example.com", phoneNumber = "345-678-9012", hireDate = "2019-07-10", jobTitle = "Marketing Specialist", salary = 50000.00, departmentId = 3 },
            new EmployeeModel { employeeId = 4, firstName = "Michael", lastName = "Brown", email = "michael.brown@example.com", phoneNumber = "456-789-0123", hireDate = "2018-10-01", jobTitle = "Research Scientist", salary = 70000.00, departmentId = 4 },
            new EmployeeModel { employeeId = 5, firstName = "Sarah", lastName = "Davis", email = "sarah.davis@example.com", phoneNumber = "567-890-1234", hireDate = "2017-11-11", jobTitle = "HR Manager", salary = 65000.00, departmentId = 1 },
            new EmployeeModel { employeeId = 6, firstName = "David", lastName = "Wilson", email = "david.wilson@example.com", phoneNumber = "678-901-2345", hireDate = "2022-05-20", jobTitle = "IT Support", salary = 48000.00, departmentId = 5 },
            new EmployeeModel { employeeId = 7, firstName = "Laura", lastName = "Martinez", email = "laura.martinez@example.com", phoneNumber = "789-012-3456", hireDate = "2023-02-14", jobTitle = "Sales Executive", salary = 52000.00, departmentId = 6 },
            new EmployeeModel { employeeId = 8, firstName = "James", lastName = "Garcia", email = "james.garcia@example.com", phoneNumber = "890-123-4567", hireDate = "2016-04-09", jobTitle = "Customer Service Rep", salary = 45000.00, departmentId = 7 },
            new EmployeeModel { employeeId = 9, firstName = "Robert", lastName = "Miller", email = "robert.miller@example.com", phoneNumber = "901-234-5678", hireDate = "2015-08-25", jobTitle = "Legal Advisor", salary = 80000.00, departmentId = 8 },
            new EmployeeModel { employeeId = 10, firstName = "Jessica", lastName = "Rodriguez", email = "jessica.rodriguez@example.com", phoneNumber = "012-345-6789", hireDate = "2021-12-30", jobTitle = "Quality Inspector", salary = 47000.00, departmentId = 10 }
        };
        public IActionResult EmployeeList()
        { 
            ViewBag.employeedata = employees;
            return View();
        }
        public IActionResult EmployeeForm()
        {
            return View();
        }
        
    }
}
