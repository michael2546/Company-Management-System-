using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentsRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork /*IDepartmentRepository departmentsRepo*/) 
        {
            //_departmentsRepo = departmentsRepo;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
                var departments = _unitOfWork.DepartmentRepository.GetAll();
                return View(departments);
        }

        [HttpGet] //the default (can be deleted)
        public IActionResult Create()
        {
            return View(); 
            //if you dont add name of view he will return the name of the action as a view (create)
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) //server side valdation
            {
                _unitOfWork.DepartmentRepository.Add(department); //num of columns affected
                var count = _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // /Department/Details/1
        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if (department is null)
                return NotFound();  //404

            return View(ViewName , department);
        }

        //Department/Edit/1
        public IActionResult Edit(int? id )
        {
            #region 
            //if (id is null)
            //    return BadRequest();

            //var department = _departmentsRepo.Get(id.Value);

            //if(department is null)
            //    return NotFound();

            //return View(department);
            #endregion

            //same code of details
            return Details(id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // to block any forign requests like post man app
        public IActionResult Edit([FromRoute] int id , Department department)
        {
            if (id != department.Id)
                return BadRequest();//if any hack to change the value of id he will return bad request

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    //1. Log Exeption
                    //2. Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete"); //same code of details
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department) 
        {
            if (id != department.Id)
                return BadRequest();//if any hack to change the value of id he will return bad request

            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty , ex.Message);
                return View(department);
            }
        }
    }
}
