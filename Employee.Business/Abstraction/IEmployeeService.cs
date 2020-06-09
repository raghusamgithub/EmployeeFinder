using AWE.Employee.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.Business.Abstraction
{
    public interface IEmployeeService
    {
        ResponseData GetEmployeeByName(string serachEmp);
    }
}
