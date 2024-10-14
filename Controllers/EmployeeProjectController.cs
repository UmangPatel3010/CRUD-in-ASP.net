using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;

namespace StaticCRUD.Controllers
{
    public class EmployeeProjectController : Controller
    {
        List<EmployeeProjectModel> employeesProjects = new List<EmployeeProjectModel>
        {
            new EmployeeProjectModel{ employeeProjectId = 1, employeeId = 1, projectId = 3 },
            new EmployeeProjectModel{ employeeProjectId = 2, employeeId = 2, projectId = 5 },
            new EmployeeProjectModel{ employeeProjectId = 3, employeeId = 3, projectId = 2 },
            new EmployeeProjectModel{ employeeProjectId = 4, employeeId = 4, projectId = 7 },
            new EmployeeProjectModel{ employeeProjectId = 5, employeeId = 5, projectId = 6 },
            new EmployeeProjectModel{ employeeProjectId = 6, employeeId = 6, projectId = 1 },
            new EmployeeProjectModel{ employeeProjectId = 7, employeeId = 7, projectId = 4 },
            new EmployeeProjectModel{ employeeProjectId = 8, employeeId = 8, projectId = 9 },
            new EmployeeProjectModel{ employeeProjectId = 9, employeeId = 9, projectId = 8 },
            new EmployeeProjectModel{ employeeProjectId = 10, employeeId = 10, projectId = 10 },
            new EmployeeProjectModel{ employeeProjectId = 11, employeeId = 1, projectId = 6 },
            new EmployeeProjectModel{ employeeProjectId = 12, employeeId = 2, projectId = 8 },
            new EmployeeProjectModel{ employeeProjectId = 13, employeeId = 3, projectId = 7 },
            new EmployeeProjectModel{ employeeProjectId = 14, employeeId = 4, projectId = 5 },
            new EmployeeProjectModel{ employeeProjectId = 15, employeeId = 5, projectId = 10 },
            new EmployeeProjectModel{ employeeProjectId = 16, employeeId = 6, projectId = 4 },
            new EmployeeProjectModel{ employeeProjectId = 17, employeeId = 7, projectId = 3 },
            new EmployeeProjectModel{ employeeProjectId = 18, employeeId = 8, projectId = 2 },
            new EmployeeProjectModel{ employeeProjectId = 19, employeeId = 9, projectId = 1 },
            new EmployeeProjectModel{ employeeProjectId = 20, employeeId = 10, projectId = 9 }
        };
        public IActionResult EmployeeProjectList()
        {
            ViewBag.employeeProjectData = employeesProjects;
            return View();
        }
        public IActionResult EmployeeProjectForm()
        {
            return View();
        }
    }
}
