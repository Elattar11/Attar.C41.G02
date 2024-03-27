using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.BLL.Repositories;
using Attar.C41.G02.DAL.Models;
using Attar.C41.G02.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Attar.C41.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepo , IWebHostEnvironment env, IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _env = env;
            _mapper = mapper;
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
        public IActionResult Create(DepartmentViewModel departmentVM)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                var count = _departmentRepo.Add(mappedDep);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(departmentVM);

        }


        [HttpGet]
        public IActionResult Details(int? id , string viewName = "Details") 
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }

            var department = _departmentRepo.Get(id.Value);

            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);

            if (department is null)
            {
                return NotFound(); //404 
            }

            return View( viewName , mappedDep);


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
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _departmentRepo.Update(mappedDep);
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

                return View(departmentVM);
                
            }
            

        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _departmentRepo.Delete(mappedDep);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {

                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Un Error Has Occured");

                }

                return View(departmentVM);
            }
        }
    }
}
