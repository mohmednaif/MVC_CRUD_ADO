using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IEmployeeService
    {
        List<Department> GetAllDepartments();
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        int SaveEmployee(Employee employee);
        bool DeleteEmployeeById(int id);

    }
}
