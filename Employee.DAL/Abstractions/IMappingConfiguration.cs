using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.DAL.Abstractions
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder moduleBuilder);
    }
}
