using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;

namespace StaticCRUD.Controllers
{
    public class DepartmentController : Controller
    {
        List<DepartmentModel> departments = new List<DepartmentModel>
        {
            new DepartmentModel{ departmentId = 1, departmentName = "Human Resources" },
            new DepartmentModel{ departmentId = 2, departmentName = "Finance" },
            new DepartmentModel{ departmentId = 3, departmentName = "Marketing" },
            new DepartmentModel{ departmentId = 4, departmentName = "Research and Development" },
            new DepartmentModel{ departmentId = 5, departmentName = "Information Technology" },
            new DepartmentModel{ departmentId = 6, departmentName = "Sales" },
            new DepartmentModel{ departmentId = 7, departmentName = "Customer Service" },
            new DepartmentModel{ departmentId = 8, departmentName = "Legal" },
            new DepartmentModel{ departmentId = 9, departmentName = "Production" },
            new DepartmentModel{ departmentId = 10, departmentName = "Quality Assurance" }
        };
        public IActionResult DepartmentList()
        {
            ViewBag.departmentData = departments;
            return View();
        }
        public IActionResult DepartmentForm(DepartmentModel dm)
        {
            if (string.IsNullOrEmpty(dm.departmentName))
                ModelState.AddModelError("departmentName", "something is missing");

            if (ModelState.IsValid)
            {
                ViewBag.departmentData = dm.departmentName;
                return View();
            }
            else
            {
                ViewBag.departmentData = "Not found";
                return View();
            }
        }
    }
}
