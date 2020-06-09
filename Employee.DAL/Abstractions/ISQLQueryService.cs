using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.DAL.Abstractions
{
   public interface ISQLQueryService
    {
        string GetQuery(string key);
    }
}
