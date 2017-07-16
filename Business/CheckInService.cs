using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Models;
using Models.DTO;

namespace Business
{
    public class CheckInService : ICheckInService
    {
        private readonly IRepository<CheckIn> _checkInRepository;
        private readonly IUnitOfWork _uow;

        public CheckInService(IRepository<CheckIn> checkInRepository, IUnitOfWork uow)
        {
            _checkInRepository = checkInRepository;
            _uow = uow;
        }

        public IEnumerable<CheckInDto> Get()
        {
            return _uow.Query<CheckIn>()
                .OrderByDescending(o => o.VisitDateTime)
                .Select(o => new CheckInDto
                {
                    CheckInId = o.Id,
                    CheckInVisitDateTime = o.VisitDateTime,
                    PersonBirthDate = o.Person.BirthDate,
                    PersonFirstName = o.Person.FirstName,
                    PersonLastName = o.Person.LastName,
                    PersonSex = o.Person.Sex
                });
        }

        public void Add(CheckInDto checkInDto)
        {
            using (var transaction = _uow.BeginTransaction())
            {
                var person = _uow
                    .Query<Person>()
                    .SingleOrDefault(p => p.LastName == checkInDto.PersonLastName &&
                                          p.FirstName == checkInDto.PersonFirstName &&
                                          p.BirthDate.Date == checkInDto.PersonBirthDate.Date &&
                                          p.Sex == checkInDto.PersonSex);
                if (person == null)
                    person = new Person
                    {
                        FirstName = checkInDto.PersonFirstName,
                        LastName = checkInDto.PersonLastName,
                        BirthDate = checkInDto.PersonBirthDate,
                        Sex = checkInDto.PersonSex
                    };

                var checkIn = new CheckIn { Person = person, VisitDateTime = DateTime.Now };
                _uow.Add(checkIn);

                transaction.Commit();
            }
        }
    }

    public interface ICheckInService
    {
        void Add(CheckInDto checkInDto);
        IEnumerable<CheckInDto> Get();
    }
}
