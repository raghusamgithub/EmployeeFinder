using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.DAL.Abstractions
{
    public interface IHelperProvider
    {
        string FormatAmount(string strAmount);
    }
}
