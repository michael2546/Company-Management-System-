using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IEmployeeRepository _employeesRepo;
       // private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IMapper mapper , IUnitOfWork unitOfWork /*, IDepartmentRepository departmentRepo*/ ) //Ask CLR for creating an object
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_employeesRepo = EmployeesRepo;
            //_departmentRepo = departmentRepo;
        }


        public IActionResult Index(string searchInp)
        {
            #region TempData ViewData ViewBag

            //keep its value when move from create to index
            TempData.Keep();

            //View Data
            ViewData["Message"] = "Hello ViewData";

            //View Bag (dynamic type property)
            ViewBag.Message = "Hello ViewBag";

            #endregion

            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(searchInp))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.SearchByName(searchInp.ToLower());

            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmps);
        }



        [HttpGet] //the default (can be deleted)
        //[ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll();

            return View();
            //if you dont add name of view he will return the name of the action as a view (create)
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) //server side valdation
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                //manual mapping
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                var count = _unitOfWork.EmployeeRepository.Add(mappedEmp); //num of columns affected

                // TempData

                _unitOfWork.Complete();
                if (count > 0)
                    TempData["Message"] = "Employee Is Created Successfully";

                else
                    TempData["Message"] = "An Error Has Occured, Employee Not Created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }



        // /Employee/Details/1
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //400

            var Employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            var mappedEmp = _mapper.Map<Employee,EmployeeViewModel >(Employee);


            if (Employee is null)
                return NotFound();  //404

            return View(ViewName, mappedEmp);
        }



        //Employee/Edit/1
        public IActionResult Edit(int? id)
        {
            #region 
            //if (id is null)
            //    return BadRequest();

            //var Employee = _EmployeesRepo.Get(id.Value);

            //if(Employee is null)
            //    return NotFound();

            //return View(Employee);
            #endregion

            //ViewBag.Departments = _departmentRepo.GetAll();


            //same code of details
            return Details(id, "Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken] // to block any forign requests like post man app
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();//if any hack to change the value of id he will return bad request

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //1. Log Exeption
                    //2. Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }



        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete"); //same code of details
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel employeeVM  )
        {
            if (id != employeeVM.Id)
                return BadRequest();//if any hack to change the value of id he will return bad request

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                var count = _unitOfWork.Complete();
                if (count > 0)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "images");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
        }


    }
}
