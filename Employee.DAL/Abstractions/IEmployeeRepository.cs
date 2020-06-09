using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.DAL.Abstractions
{
    public interface IEmployeeRepository
    {
        List<Employee.DAL.Entities.Employee> GetEmployeeSearchDetails(string searchEmp);
    }
}
