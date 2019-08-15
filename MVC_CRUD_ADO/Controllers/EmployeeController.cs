using Domain;
using Infrastructure;
using MVC_CRUD_ADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CRUD_ADO.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        // GET: Employee
        public ViewResult Index()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Employees = GetEmployees();
            FillDdlDepartment();
            return View(employeeViewModel);
        }
        public PartialViewResult GetEmployeeList()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Employees = GetEmployees();
            return PartialView("_EmployeeListPartial", employeeViewModel);
        }

        public void FillDdlDepartment()
        {
            var departments = employeeService.GetAllDepartments();
            ViewBag.DdlDepartment = new SelectList(departments, "Id", "DepartmentName");
        }

        public List<Employee> GetEmployees()
        {
            return employeeService.GetAllEmployees();
        }

        public PartialViewResult SaveEmployee(EmployeeViewModel employeeViewModel)
        {
            employeeService.SaveEmployee(employeeViewModel.Employee);
            ModelState.Clear();
            FillDdlDepartment();
            return PartialView("_AddEmployeePartial", new EmployeeViewModel());
        }

        public PartialViewResult EditEmployee(int id)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Employee = employeeService.GetEmployeeById(id);

            FillDdlDepartment();
            return PartialView("_AddEmployeePartial", employeeViewModel);
        }

        public PartialViewResult AddEmployee()
        {
            FillDdlDepartment();
            return PartialView("_AddEmployeePartial", new EmployeeViewModel());
        }

        public JsonResult DeleteEmployee(int id)
        {
            employeeService.DeleteEmployeeById(id);
            return Json(true);
        }


    }
}