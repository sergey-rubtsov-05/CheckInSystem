using System;
using System.Collections.Generic;
using System.Linq;
using Business.Exceptions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<CheckIn> Get()
        {
            return GetQuery()
                .OrderByDescending(o => o.VisitDateTime);
        }

        private IQueryable<CheckIn> GetQuery()
        {
            return _uow.Query<CheckIn>()
                .Include(o => o.Person)
                .Where(o => o.IsDeleted == false);
        }

        public void Delete(int id)
        {
            var checkIn = _uow.Query<CheckIn>().FirstOrDefault(o => o.Id == id);

            if (checkIn == null)
                throw new NotFoundExpection("Удаляемое посещение не найдено");

            if (checkIn.IsDeleted)
                return;

            checkIn.IsDeleted = true;
            _uow.SaveChanges();
        }

        public CheckIn Update(int id, CheckInDto checkInDto)
        {
            var checkIn = GetQuery().FirstOrDefault(o => o.Id == id);

            if (checkIn == null)
                throw new NotFoundExpection("Изменяемое посещение не найдено");

            checkIn.Person.FirstName = checkInDto.PersonFirstName;
            checkIn.Person.LastName = checkInDto.PersonLastName;
            checkIn.Person.BirthDate = checkInDto.PersonBirthDate.Date;
            checkIn.Person.Sex = checkInDto.PersonSex;

            _uow.SaveChanges();

            return checkIn;
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
        IQueryable<CheckIn> Get();
        void Delete(int id);
        CheckIn Update(int id, CheckInDto checkInDto);
    }
}
