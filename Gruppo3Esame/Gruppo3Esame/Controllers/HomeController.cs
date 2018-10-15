using Microsoft.AspNetCore.Mvc;
using Gruppo3Esame.Models;
using Gruppo3Esame.DataAccess;
using System.Linq;

namespace Gruppo3Esame.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Project> _projectRepository;

        public HomeController(IRepository<Employee> employeeRepository, IRepository<Project> projectRepository)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
