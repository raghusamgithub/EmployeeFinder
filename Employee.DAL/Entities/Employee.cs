using AWE.Employee.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.DAL.Entities
{
    public class Employee : BaseEntity
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
