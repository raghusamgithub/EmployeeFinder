using AutoMapper;
using AWE.Employee.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWE.Employee.DAL.Entities;

namespace AWE.Employee.Business.MapperConfiguration
{
    public class DTOMapperConfiguration : Profile
    {
        public DTOMapperConfiguration()
        {
            //TODO Call the method added below for Mapping
            CreateDiversionMapper();
        }

        protected virtual void CreateDiversionMapper()
        {
            //CreateMap<DiversionResultSet, DiversionDTO>()
            //     .ForMember(x => x.InEligibilityReasons, y => y.MapFrom(z => GetList(z.InEligibilityReasons, ", ")));
            //CreateMap<DiversionStatusResultSet, DiversionStatusDTO>();
            //CreateMap<CodeTableFlagResultSet, CodeTableFlagResultSetDTO>();
            //CreateMap<DiversionConditionResultSet, DiversionConditionDTO>();
            //CreateMap<DiversionLookupResultSet, DiversionLookupDTO>();
            //CreateMap<PropertyDTO, Property>();
            //CreateMap<Property, PropertyDTO>();
            //CreateMap<PropertyDTO, PropertyDispo>();
            //CreateMap<PropertyDispo, PropertyDTO>();
            CreateMap<EmployeeDTO, AWE.Employee.DAL.Entities.Employee>();
            CreateMap<AWE.Employee.DAL.Entities.Employee, EmployeeDTO>();

        }

        private List<string> GetList(string model, string splitChar)
        {

            if (string.IsNullOrEmpty(model))
            {
                return null;
            }

            return model.Split(splitChar).ToList();

        }
    }
}
