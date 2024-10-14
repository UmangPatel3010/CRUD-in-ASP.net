using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;

namespace StaticCRUD.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult ProjectList()
        {
            var projects = new List<ProjectModel>
            {
                new ProjectModel{ projectId = 1, projectName = "Website Redesign", startDate = "2023-01-01", endDate = "2023-06-30", budget = 15000.00 },
                new ProjectModel{ projectId = 2, projectName = "Market Research", startDate = "2023-02-15", endDate = "2023-08-15", budget = 25000.00 },
                new ProjectModel{ projectId = 3, projectName = "New Product Development", startDate = "2022-11-01", endDate = "2023-12-31", budget = 50000.00 },
                new ProjectModel{ projectId = 4, projectName = "Employee Training Program", startDate = "2023-03-01", endDate = "2023-09-30", budget = 10000.00 },
                new ProjectModel{ projectId = 5, projectName = "IT Infrastructure Upgrade", startDate = "2023-04-10", endDate = "2023-10-10", budget = 30000.00 },
                new ProjectModel{ projectId = 6, projectName = "Customer Feedback Analysis", startDate = "2023-05-01", endDate = "2023-11-30", budget = 12000.00 },
                new ProjectModel{ projectId = 7, projectName = "Financial Audit", startDate = "2023-01-15", endDate = "2023-07-15", budget = 20000.00 },
                new ProjectModel{ projectId = 8, projectName = "Sales Campaign", startDate = "2023-06-01", endDate = "2023-12-31", budget = 18000.00 },
                new ProjectModel{ projectId = 9, projectName = "Legal Compliance Review", startDate = "2023-03-20", endDate = "2023-08-20", budget = 22000.00 },
                new ProjectModel{ projectId = 10, projectName = "Quality Control Improvement", startDate = "2023-07-01", endDate = "2023-12-31", budget = 14000.00 }
            };
            ViewBag.projectdata = projects;
            return View();
        }
        public IActionResult ProjectForm()
        {
            return View();
        }
    }
}
