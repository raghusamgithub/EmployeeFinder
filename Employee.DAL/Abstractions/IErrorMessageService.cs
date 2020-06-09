using AWE.Employee.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.DAL.Abstractions
{
    public partial interface IErrorMessageService
    {
        string GetErrorMessages(ErrorMessage errorMessage, params string[] caption);
    }
}
