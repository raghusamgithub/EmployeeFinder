using AWE.Employee.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.Business.DTO
{
    public class EmployeeDTO : BaseDTO
    {
        public int EmployeeId {get;set;}
        public string EmployeeName { get; set; }

    }
}
