using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AWE.Employee.DAL.Abstractions
{
    public interface IDataProvider
    {
        DbParameter GetParameter();
    }
}
