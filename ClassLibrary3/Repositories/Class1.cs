using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassLibrary1.Data;

namespace ClassLibrary1.Repositories
{
   public class Class1
    {

        public async Task<List<Person>> Get()
        {
            using Data.Database_FinesContext database_FinesContext = new Database_FinesContext();
            return await database_FinesContext.People.ToListAsync();
        }
        public async Task<Person> Get(long id)
        {
            using Data.Database_FinesContext database_FinesContext = new Database_FinesContext();
            return await database_FinesContext.People.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task Delete(Person person )
        {
            using Data.Database_FinesContext database_FinesContext = new Database_FinesContext();
           var personToRemove=await database_FinesContext.People.FirstOrDefaultAsync(i => i.Id == person.Id);
            if(personToRemove!=null)
            {
                database_FinesContext.People.Remove(personToRemove);
            }
            await database_FinesContext.SaveChangesAsync();

        }
        public async Task AddFine(long id, FinesInputViewModel finesInputViewModel )
        {
            using Data.Database_FinesContext database_FinesContext = new Database_FinesContext();
           
           
                if (finesInputViewModel != null)
                {
                    var car = await database_FinesContext.Cars.FirstOrDefaultAsync(c => c.Number == finesInputViewModel.CarNumber);
                    if (car == null)
                    {
                      //  return null;
                    }
                    var registrator = await database_FinesContext.Registrators.FirstOrDefaultAsync(r => r.GerNumber == finesInputViewModel.NumberRegistrator);
                    if (registrator == null)
                    {
                        registrator = new Registrator
                        {
                            GerNumber = finesInputViewModel.NumberRegistrator,

                        };
                    }
                    if (finesInputViewModel.IsPersonal)
                    {
                        var person = await database_FinesContext.People.Include(f => f.Fines).FirstOrDefaultAsync(a => a.Id == id);
                        if (person == null)
                        {
                           // return null;
                        }
                        if (person != null)
                        {

                            if (car != null)
                            {

                                if (registrator != null)
                                {
                                    person.Fines.Add(new Fine
                                    {
                                        DateTmeOfAccident = finesInputViewModel.DateTime,
                                        Driver = person,
                                        DriverId = person.Id,
                                        Car = car,
                                        CarId = car.Id,
                                        IsActive = true,
                                        IsPersonal = finesInputViewModel.IsPersonal,
                                        Value = finesInputViewModel.Value,
                                        Registrator = registrator,
                                        RegistratorId = registrator.Id,

                                    });
                                database_FinesContext.People.Update(person);
                                }
                            }
                        }
                    }
                    if (!finesInputViewModel.IsPersonal)
                    {
                        var owner = await database_FinesContext.People.Include(p => p.PersonCars).Include(f => f.Fines).Where(a => a.PersonCars
                            .Any(q => q.CarId == car.Id) && a.PersonCars
                             .Any(s => s.DateFrom.CompareTo(finesInputViewModel.DateTime) <= 0 && a.PersonCars.Any(z => z.DateTo
                                 .CompareTo(finesInputViewModel.DateTime) >= 0))).FirstOrDefaultAsync();

                        car.Fines?.Add(new Fine
                        {
                            Car = car,
                            Driver = owner,
                            DateTmeOfAccident = finesInputViewModel.DateTime,
                            DriverId = owner.Id,
                            CarId = car.Id,
                            IsActive = true,
                            Value = finesInputViewModel.Value,
                            IsPersonal = false,
                            Registrator = registrator,
                            RegistratorId = registrator.Id
                        });
                    database_FinesContext.Cars.Update(car);

                    
                }
            }
        }

        public async Task<PersonDataOutput[]> GetPersonsData()
        {
            using Data.Database_FinesContext database_FinesContext = new Database_FinesContext();
            var people = await database_FinesContext.People.Include(f => f.Fines).Include(p => p.PersonCars).ToListAsync();
            var fines = await database_FinesContext.Fines.Include(s => s.Car).Include(d => d.Driver).ToListAsync();

            var cars = await database_FinesContext.Cars.Include(a => a.PersonCars).Include(s => s.Fines).ToListAsync();
            List<PersonDataOutput> personDataOutputs = new();
            for (int i = 0; i < people.Count; i++)
            {
                var allFinesCount = 0;
                var activeFinesCount = 0;

                var finesPersonal = fines.Where(a => a.IsPersonal && a.DriverId == people[i].Id).ToList();
                activeFinesCount += finesPersonal.Where(a => a.IsActive).Count();
                allFinesCount += finesPersonal.Count;
                var allCars = people[i].PersonCars.Count;

                var activeCarsCount = people[i].PersonCars.Where(j => j.DateFrom.CompareTo(DateTime.Now) <= 0
                     && j.DateTo.CompareTo(DateTime.Now) >= 0
                ).ToList().Count;

                for (int j = 0; j < people[i].PersonCars.Count; j++)
                {

                    var finesByOwner = fines.Where(s => s.DateTmeOfAccident.CompareTo(people[i].PersonCars.ElementAt(j).DateFrom) >= 0
                            && s.DateTmeOfAccident.CompareTo(people[i].PersonCars.ElementAt(j).DateTo) <= 0

                            && !s.IsPersonal
                    ).ToList();

                    allFinesCount += finesByOwner.Count;
                    activeFinesCount += finesByOwner.Where(a => a.IsActive).Count();



                }
                PersonDataOutput personDataOutput = new()
                {
                    CarData = $"{activeCarsCount}/{allCars}",
                    FineData = $"{activeFinesCount}/{allFinesCount}",
                    City = people[i].City,
                    Address = people[i].Address,
                    Surname = people[i].Surname,
                    Id = people[i].Id
                };

                personDataOutputs.Add(personDataOutput);
            }
            return personDataOutputs.ToArray();

        }
        public async Task<PersonCarFineDataOutput> GetPersonData(long id)
        {
            using Data.Database_FinesContext database_FinesContext = new Database_FinesContext();
            var person = await database_FinesContext.People.Include(f => f.Fines).Include(p => p.PersonCars).FirstOrDefaultAsync(i => i.Id == id);
            if (person != null)
            {
                var fines = await database_FinesContext.Fines.Include(a => a.Car).Include(d => d.Driver).Where(w => w.DriverId == id).ToListAsync();
                var personCars = await database_FinesContext.PersonCars.Include(p => p.Car).Include(c => c.Person).Where(a => a.PersonId == person.Id).ToListAsync();

                var activeCars = personCars.Where(a => a.DateFrom.CompareTo(DateTime.Now) <= 0 && a.DateTo.CompareTo(DateTime.Now) >= 0).ToList();
                List<FineDataOutput> finesOutput = new List<FineDataOutput>();
                List<CarDataOutput> carsOutput = new List<CarDataOutput>();
                for (int i = 0; i < fines.Count; i++)
                {
                    FineDataOutput fineDataOutput = new FineDataOutput
                    {
                        CarId = fines[i].CarId,
                        IsActive = fines[i].IsActive,
                        Number = fines[i].Car.Number,
                        Value = fines[i].Value,
                        Id = fines[i].Id

                    };
                    finesOutput.Add(fineDataOutput);

                }
                for (int i = 0; i < personCars.Count; i++)
                {

                    CarDataOutput carDataOutput = new CarDataOutput
                    {
                        BeginOwning = personCars[i].DateFrom,
                        EndOwning = personCars[i].DateTo,
                        Name = personCars[i].Car.Name,
                        Number = personCars[i].Car.Number,
                        Id = personCars[i].CarId
                    };
                    carsOutput.Add(carDataOutput);
                }



                PersonCarFineDataOutput personDataOutput = new PersonCarFineDataOutput
                {
                    City = person.City,
                    Address = person.Address,
                    Surname = person.Surname,
                    Id = person.Id,
                    CarData = carsOutput.ToArray(),
                    FineDatas = finesOutput.ToArray()
                };
                return personDataOutput;
            }
            return null;
        }
    }
}


