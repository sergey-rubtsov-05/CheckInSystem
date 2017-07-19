using AutoMapper;
using Models.DTO;

namespace Models.Maps
{
    public class CheckInMapConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<CheckIn, CheckInDto>();
            map.ForMember(o => o.CheckInId, expression => expression.MapFrom(o => o.Id));
            map.ForMember(o => o.PersonFirstName, expression => expression.MapFrom(o => o.Person.FirstName));
            map.ForMember(o => o.PersonLastName, expression => expression.MapFrom(o => o.Person.LastName));
            map.ForMember(o => o.PersonBirthDate, expression => expression.MapFrom(o => o.Person.BirthDate));
            map.ForMember(o => o.PersonSex, expression => expression.MapFrom(o => o.Person.Sex));
            map.ForMember(o => o.CheckInVisitDateTime, expression => expression.MapFrom(o => o.VisitDateTime));
        }
    }

    public interface IAutoMapperTypeConfigurator
    {
        void Configure(IMapperConfigurationExpression configuration);
    }
}