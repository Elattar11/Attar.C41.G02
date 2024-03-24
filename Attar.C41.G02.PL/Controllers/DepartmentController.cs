using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.BLL.Repositories;
using Attar.C41.G02.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Attar.C41.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepo , IWebHostEnvironment env)
        {
            _departmentRepo = departmentRepo;
            _env = env;
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
        public IActionResult Details(int? id , string viewName = "Details") 
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }

            var department = _departmentRepo.Get(id.Value);

            if (department is null)
            {
                return NotFound(); //404 
            }

            return View( viewName ,department);


        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id , "Edit");

            ///if (!id.HasValue)
            ///{
            ///    return BadRequest(); 
            ///}
            ///
            ///var department = _departmentRepo.Get(id.Value);
            ///
            ///if (department is null)
            ///{
            ///    return NotFound(); 
            ///}
            ///
            ///return View();
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _departmentRepo.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                //1. Log Exception

                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Un Error Has Occured");

                }

                return View(department);
                
            }
            

        }
    }
}
