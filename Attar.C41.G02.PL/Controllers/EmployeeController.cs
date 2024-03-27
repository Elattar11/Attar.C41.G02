using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.DAL.Models;
using Attar.C41.G02.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Attar.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepo, IWebHostEnvironment env 
            , IMapper mapper
            /*, IDepartmentRepository departmentRepository*/)
        {
            _employeeRepo = employeeRepo;
            _env = env;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        }
        public IActionResult Index(string searchInp)
        {
            //ViewData["Message"] = "Hello ViewData";

            //ViewBag.Message = "Hello ViewBag"; 


            var employees = Enumerable.Empty<Employee>();

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            if (string.IsNullOrEmpty(searchInp))
                employees = _employeeRepo.GetAll();
            else
                employees = _employeeRepo.searchByName(searchInp.ToLower());
            return View(mappedEmp); 

        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepository.GetAll();
            return View();

        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel , Employee>(employeeVM);

                var count = _employeeRepo.Add(mappedEmp);


                if (count > 0)
                {
                    TempData["Message"] = "Department is created successfully"; 
                }
                else
                {
                    TempData["Message"] = "Error !!!";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(employeeVM);

        }


        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }

            var employee = _employeeRepo.Get(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
            {
                return NotFound(); //404 
            }

            return View(viewName, mappedEmp);


        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepository.GetAll();
            return Details(id, "Edit");

            ///if (!id.HasValue)
            ///{
            ///    return BadRequest(); 
            ///}
            ///
            ///var employee = _employeeRepo.Get(id.Value);
            ///
            ///if (employee is null)
            ///{
            ///    return NotFound(); 
            ///}
            ///
            ///return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepo.Update(mappedEmp);
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

                return View(employeeVM);

            }


        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepo.Delete(mappedEmp);
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

                return View(employeeVM);
            }
        }
    }
}
