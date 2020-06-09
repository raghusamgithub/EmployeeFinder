using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace AWE.Employee.Common.Utilities
{
   public class AutoMapperConfiguration
    {
        public static IMapper Mapper { get; private set; }
        public static AutoMapper.MapperConfiguration MapperConfiguration { get; private set; }

        public static void Init(AutoMapper.MapperConfiguration config)
        {
            MapperConfiguration = config;
            Mapper = config.CreateMapper();
        }
    }
}
