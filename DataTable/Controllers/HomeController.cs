using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTable.DataAccess;
using DataTable.Models;
using DataTable.Utilities;
using AutoMapper;

namespace DataTable.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServerSide()
        {
            return View();
        }

        public JsonResult GetEmployee(DataTableParameterModel dataTableParamModel, string searchSSN, string searchName)
        {
            IQueryable<Employee> filterdEmployees;
            List<Employee> sortedEmployees;
            List<EmployeeModel> employeeModels;

            using (EmployeeEntities context = new EmployeeEntities()) {
                filterdEmployees = context.Employees.Where(e => ((e.FirstName.Contains(searchName) ||
                                                                  e.MidName.Contains(searchName) ||
                                                                  e.LastName.Contains(searchName))
                                                               && e.SSN.Contains(searchSSN)));

                sortedEmployees = SortEmployees(dataTableParamModel, filterdEmployees).Skip(dataTableParamModel.Start)
                                                                                      .Take(dataTableParamModel.Length)
                                                                                      .ToList();

                Mapper.CreateMap<Employee, EmployeeModel>()
                      .ForMember(dest => dest.DisplayBirthDay,
                                  opt => opt.MapFrom(src => Converter.ConvertDateTimeToString(src.Birthday)));

                employeeModels = Mapper.Map<List<EmployeeModel>>(sortedEmployees);

                DataTableReturnModel<EmployeeModel> result = new DataTableReturnModel<EmployeeModel>() {
                    draw = dataTableParamModel.Draw,
                    data = employeeModels,
                    recordsTotal = filterdEmployees.Count(),
                    recordsFiltered = filterdEmployees.Count()
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        private IOrderedEnumerable<Employee> SortEmployees(DataTableParameterModel dataTableParamModel, IQueryable<Employee> employees)
        {
            IOrderedEnumerable<Employee> orderedEmployees;

            if (dataTableParamModel.Order[0].Dir == DataTableOrder.ASC) {
                orderedEmployees = employees.OrderBy(SelectSortingColumn(dataTableParamModel.Columns[dataTableParamModel.Order[0].Column].Data));
            } else {
                orderedEmployees = employees.OrderByDescending(SelectSortingColumn(dataTableParamModel.Columns[dataTableParamModel.Order[0].Column].Data));
            }

            for (var i = 1; i < dataTableParamModel.Order.Count; i++) {
                if (dataTableParamModel.Order[i].Dir == DataTableOrder.ASC)
                    orderedEmployees = orderedEmployees.ThenBy(SelectSortingColumn(dataTableParamModel.Columns[dataTableParamModel.Order[i].Column].Data));
                else
                    orderedEmployees = orderedEmployees.ThenByDescending(SelectSortingColumn(dataTableParamModel.Columns[dataTableParamModel.Order[i].Column].Data));
            }

            return orderedEmployees;
        }

        private Func<Employee, Object> SelectSortingColumn(string sortColumn)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            
            if (sortColumn == Converter.GetPropertyName(() => employeeModel.FullName))
                return item => item.FirstName;
            else if (sortColumn == Converter.GetPropertyName(() => employeeModel.DisplayBirthDay))
                return item => item.Birthday;
            else if (sortColumn == Converter.GetPropertyName(() => employeeModel.Salary))
                return item => item.Salary;
            else if (sortColumn == Converter.GetPropertyName(() => employeeModel.DepartmentName))
                return item => item.Department.Name;
            else
                return item => item.SSN;
        }
    }
}