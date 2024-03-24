using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.BLL.Repositories;
using Attar.C41.G02.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Attar.C41.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                var count = _departmentRepo.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);

        }


        [HttpGet]
        public IActionResult Details(int? id) 
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var department = _departmentRepo.Get(id.Value);

            if (department is null)
            {
                return NotFound(); //404 
            }

            return View(department);


        }
    }
}
