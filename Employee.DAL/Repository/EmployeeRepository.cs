using AWE.Employee.DAL.Abstractions;
using AWE.Employee.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Linq;
using Employee;

namespace AWE.Employee.DAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ISQLQueryService _sqlQueryService;
        private readonly IStoredProcService _storedProcService;    
        private readonly IRepository<Entities.Employee> _employee;

        public EmployeeRepository(IStoredProcService storedProcService,
            ISQLQueryService sqlQueryService, IRepository<Entities.Employee> employee)
        {
            _storedProcService = storedProcService;
            _sqlQueryService = sqlQueryService;
            _employee = employee;
        }

        public List<Entities.Employee> GetEmployeeSearchDetails(string searchEmp)
        {
            List<Entities.Employee> emp = new List<Entities.Employee>();

            emp = _employee.Table.Where(x => x.EmployeeName.Contains(searchEmp)).Select(x=>x).ToList();
            return emp;
        }

        public List<Entities.Employee> GetAllEmployee(int id)
        {
            List<Entities.Employee> emp = new List<Entities.Employee>();

            if(id > 0)
            {
                 emp = _employee.Table.Where(x => x.EmployeeId==id).Select(x => x).ToList();
            }
            else
            {
                emp = _employee.Table.Select(x => x).ToList();
            }
           
            return emp;
        }
      
        

    }
}
