using AWE.Employee.Business.Abstraction;
using AWE.Employee.Business.DTO;
using AWE.Employee.DAL.Abstractions;
using AWE.Employee.DAL.Entities;
using AWE.Employee.Common;
using AWE.Employee.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AWE.Employee.Business.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public ResponseData GetEmployeeByName(string serachEmp)
        {
            ResponseData response = new ResponseData();
            try
            {
                List<EmployeeDTO> empDTO = new List<EmployeeDTO>();

                var empDTO1 = _employeeRepository.GetEmployeeSearchDetails(serachEmp).ToEntityDTO<Employee.DAL.Entities.Employee,EmployeeDTO>();
                response.data = empDTO1;
                response.global = GlobalConstant.Sucess;
                response.inline = "Get Employee Data";
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
