using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Model;
using Microsoft.EntityFrameworkCore;
using Project5.Models;


namespace Project5.Services
{
    public class PersonRepo
    {
        public IQueryable<Person> People { get; set; } = Database_FinesContext?.People;
        static Database_FinesContext Database_FinesContext { get; set; }

        public Database_FinesContext Database;
        public PersonRepo(Database_FinesContext databaseFinesContext)
        {
            Database = databaseFinesContext;
        }

        public async Task<PersonCarFineDataOutput> GetPerson(long id)
        {
            var r = await Database.People.Include(a => a.PersonCars).ThenInclude(f => f.Car).FirstOrDefaultAsync(i => i.Id == id);

            var cars = new List<Car>();
            var fines = new List<Fine>();
            var _cars = await Database.Cars.ToListAsync();
            var nFines = await Database.Fines.ToListAsync();

            for (int i = 0; i < r.PersonCars.Count; i++)
            {
                cars.AddRange(await Database.Cars.Where(q => q.Id == r.PersonCars.ElementAt(i).CarId).ToListAsync());
                fines.AddRange(await Database.Fines.Where(q => q.CarId == cars[i].Id).ToListAsync());

            }
            var arr = cars.ToArray();
            var a = fines.ToArray();
            //  ModelUpdate modelUpdate = new ModelUpdate() {
            //      Cars = arr, Fines = a };
            PersonCarFineDataOutput personCarFineDataOutput = new PersonCarFineDataOutput();
            personCarFineDataOutput.Surname = r.Surname;
            personCarFineDataOutput.Id = r.Id;
            personCarFineDataOutput.City = r.City;
            personCarFineDataOutput.Address = r.Address;
            List<FineDataOutput> finesData = new List<FineDataOutput>();
            for (int i = 0; i < a.Length; i++)
            {
                FineDataOutput fineDataOutput = new FineDataOutput();
                fineDataOutput.Value = a[i].Value;
                fineDataOutput.IsActive = a[i].IsActive;
                fineDataOutput.Id = a[i].Id;
                fineDataOutput.CarId = a[i].CarId;

                var number = await Database.Cars.Where(q => q.Id == a[i].CarId).FirstOrDefaultAsync();
                if (number != null)
                {
                    fineDataOutput.Number = number.Number;
                }

                finesData.Add(fineDataOutput);
            }
            List<CarDataOutput> carDatas = new List<CarDataOutput>();

            for (int i = 0; i < arr.Length; i++)
            {
                CarDataOutput carDataOutput = new CarDataOutput();
                carDataOutput.Id = arr[i].Id;
                carDataOutput.Name = arr[i].Name;
                carDataOutput.Number = arr[i].Number;
                var ddd = await Database.PersonCars.Where(s => s.CarId == arr[i].Id).FirstOrDefaultAsync();
                carDataOutput.BeginOwning = ddd.DateFrom;
                carDataOutput.EndOwning = ddd.DateTo;
                carDatas.Add(carDataOutput);


            }
            personCarFineDataOutput.FineDatas = finesData.ToArray();
            personCarFineDataOutput.CarData = carDatas.ToArray();


            //  return r;
            return personCarFineDataOutput;
            //   return modelUpdate;

        }
        public async Task CreatePerson(Person person)
        {
            await Database.People.AddAsync(person);
            await Database.SaveChangesAsync();
        }
        public async Task DeletePerson(Person person)
        {
            Database.People.Remove(person);
            await Database.SaveChangesAsync();
        }

        public async Task DeletePerson(int id)
        {
            var toDelete = await Database.People.FirstOrDefaultAsync(i => i.Id == id);
            Database.People.Remove(toDelete);
            await Database.SaveChangesAsync();
        }

        public async Task Edit(Person person)
        {

            Database.People.Update(person);
            await Database.SaveChangesAsync();
        }
        public async Task Edit(PersonUpdateData person)
        {
            var personToEdit = await Database.People.FirstOrDefaultAsync(i => i.Id == person.Id);
            personToEdit.Surname = person.Surname;
            personToEdit.City = person.City;
            personToEdit.Address = person.Address;
            //will add update dependence tables
            Database.Update(personToEdit);
            await Database.SaveChangesAsync();
        }
        public async Task<List<Person>> GetPeopleAsync()
        {


            return await Database.People.Include(i => i.PersonCars).ToListAsync();
        }
        public async Task<List<Car>> GetCarsAsync()
        {
            return await Database.Cars.Include(i => i.Fines).ToListAsync();
        }
        public async Task<IEnumerable<PersonDataOutput>> GetAllPersons()
        {

            if (Database.People.Count() == 0)
            {
                await Database.People.AddAsync(new Person()
                {
                    Surname = "NewUser",
                    Address = "Street 13",
                    City = "NewCity",
                    PersonCars = new List<PersonCar>() { new PersonCar() { DateFrom= DateTime.Now.AddDays(-100), DateTo=DateTime.Now.AddDays(200),
                    Car=new Car()
                    {
                        Name="Ford", Fines=new List<Fine>()
                        {
                            new Fine()
                            {
                                DateTmeOfAccident=DateTime.Now, IsActive=true, Registrator=new Registrator()
                                {
                                    GerNumber="jfdsfj14"
                                }
                                ,Value=10
                            }
                        },
                        Number="414aaf"
                    },

                    },
                    new PersonCar()
                    {


                        DateFrom= DateTime.Now.AddDays(-200), DateTo=DateTime.Now.AddDays(300),
                    Car=new Car()
                    {
                        Name="Toyota", Fines=new List<Fine>()
                        {
                            new Fine()
                            {
                                DateTmeOfAccident=DateTime.Now.AddDays(-10), IsActive=false, Registrator=new Registrator()
                                {
                                    GerNumber="rg44rw"
                                }
                                ,Value=145.13M
                            }
                        },
                         Number="13fffa"
                    },


                    }


                    }

                }





                  );
            }
            await Database.SaveChangesAsync();


            var all = await GetPeopleAsync();
            var cars = await GetCarsAsync();

            List<PersonDataOutput> outputs = new List<PersonDataOutput>();
            for (int i = 0; i < all.Count; i++)
            {
                PersonDataOutput personDataOutput = new PersonDataOutput();
                //  personDataOutput.Person = all[i];
                personDataOutput.Id = all[i].Id;
                personDataOutput.Surname = all[i].Surname;
                personDataOutput.City = all[i].City;
                personDataOutput.Address = all[i].Address;
                var count = all[i].Fines.Count;

                var allCarsCount = all[i].PersonCars.Count;
                int currentCarCount = 0;
                var finesCount = 0;
                var activeFines = 0;
                for (int j = 0; j < allCarsCount; j++)
                {
                    var begin = all[i].PersonCars.ElementAt(j).DateFrom;
                    var end = all[i].PersonCars.ElementAt(j).DateTo;

                    var now = DateTime.Now;
                    int fisrt_check = DateTime.Compare(begin, now);
                    int second_check = DateTime.Compare(end, now);
                    if ((fisrt_check <= 0) && (second_check >= 0))
                    {
                        currentCarCount++;
                        var s = cars.FirstOrDefault(qq => qq.Id == all[i].PersonCars.ElementAt(j).CarId);
                        for(int z=0; z<s.Fines.Count;z++)
                        {
                            var firstFinesCheck = DateTime.Compare(begin, s.Fines.ElementAt(z).DateTmeOfAccident);
                            var sedondFinesCheck= DateTime.Compare(end, s.Fines.ElementAt(z).DateTmeOfAccident);
                            if((firstFinesCheck<=0) &&(sedondFinesCheck>=0))
                            {
                                finesCount++;
                                if(s.Fines.ElementAt(z).IsActive)
                                {
                                    activeFines++;
                                }
                            }
                        }
                      //  finesCount += s.Fines.Count;
                        // finesCount += all[i].PersonCars.ElementAt(j).Car.Fines.Count();
                        //activeFines+=all[i].PersonCars.ElementAt(j).Car.Fines.Where(q => q.IsActive).Count();
                        //activeFines += (s.Fines.Where(act => act.IsActive).Count());
                        //all[i].Fines=   all[i].Fines.Union(all[i].PersonCars.ElementAt(j).Car.Fines);
                    }
                    else
                    {
                        var carInfo = cars.FirstOrDefault(c => c.Id == all[i].PersonCars.ElementAt(j).CarId);
                        carInfo.Fines.Select(a => a.DateTmeOfAccident);
                        var finesCounts = all[i].Fines.Count;
                        for (int abc = 0; abc < finesCounts; abc++)
                        {
                            var checkBegin = DateTime.Compare(all[i].PersonCars.ElementAt(j).DateFrom, all[i].Fines.ElementAt(abc).DateTmeOfAccident);
                            var checkEnd = DateTime.Compare(all[i].PersonCars.ElementAt(j).DateTo, all[i].Fines.ElementAt(abc).DateTmeOfAccident);
                            if((checkBegin<=0) &&(checkEnd>=0))
                            {
                                finesCount++;
                                if(all[i].Fines.ElementAt(abc).IsActive)
                                {
                                    activeFines++;
                                }
                            }
                                
                                
                        }
                    }
                }
                int activeCount = all[i].Fines.Where(i => i.IsActive).Count();

                personDataOutput.FineData = $"{activeFines}/{finesCount}";
                personDataOutput.CarData = $"{currentCarCount}/{allCarsCount}";
                outputs.Add(personDataOutput);

            }
            return outputs;

        }
        public async Task<List<object>> GeAllPeopleAsync()
        {
            var tmp = await Database.People.ToListAsync();
            for (int i = 0; i < tmp.Count; i++)
            {
                Object f = (object)tmp;


            }
            throw new NotImplementedException();
        }


        public async Task CreateAccount()
        {

        }

        public async Task AddCar(long id, CarDataInput car)
        {
            //var _p = await Database.People.Include(a => ();            
            var person = await Database.People.Include(a => a.PersonCars).Include(q => q.Fines).FirstOrDefaultAsync(i => i.Id == id);

            if (person != null)
            {
                person.PersonCars.Add(new PersonCar()
                {
                    Car = new Car() { Name = car.name, Number = car.number },
                    DateFrom = car.date,
                    DateTo = car.date.AddDays(3650)
                }

                    );
            }
            Database.People.Update(person);
            await Database.SaveChangesAsync();
        }

        public async Task ChangeOwner(long id, ChangeCarOwnerData changeCarOwnerViewOwner)
        {
            var car = await Database.Cars.FirstOrDefaultAsync(i => i.Id == id);
            var newOwner = await Database.People.Include(a=>a.PersonCars).Include(q=>q.Fines).FirstOrDefaultAsync(a => a.Id == changeCarOwnerViewOwner.Id);
            var people = await Database.People.ToListAsync();
            var person = Database.PersonCars.Where(i => i.Car == car).Where(a => (a.DateFrom.CompareTo(DateTime.Now) <= 0) &&
                (a.DateTo.CompareTo(DateTime.Now) >= 0)).Select(x => x.Person);
         

           

            var fPerson = await person.FirstOrDefaultAsync();
            var oldPerson =await   Database.People.Include(z => z.PersonCars).Include(a => a.Fines).FirstOrDefaultAsync(i => i.Id == fPerson.Id);
            if ((newOwner != null) && (oldPerson != null))
            {

                for (int i = 0; i < oldPerson.PersonCars.Count; i++)
                {
                    if (oldPerson.PersonCars.ElementAt(i).Car == car)
                    {
                        oldPerson.PersonCars.ElementAt(i).DateTo = changeCarOwnerViewOwner.Date;
                        break;
                    }

                }
                //person.PersonCars.Where(a => a.Car == car).FirstOrDefault().DateTo = changeCarOwnerViewOwner.DateTime;
                //newOwner.PersonCars.Where(a => a.Car == car).FirstOrDefault().DateFrom = changeCarOwnerViewOwner.DateTime.AddSeconds(100);
                //newOwner.PersonCars.Where(a => a.Car == car).FirstOrDefault().DateTo = DateTime.Now.AddYears(10);
                //   var p= person.PersonCars.Where(a => a.Car == car).FirstOrDefault();

            }
            //    Person person = new Person();
            //    for (int i = 0; i < people.Count; i++)
            //    {


            //        for (int j = 0; j < people[i].PersonCars.Count; j++)
            //        {
            //            if ((people[i].PersonCars.ElementAt(j).DateFrom.CompareTo(DateTime.Now) <= 0) && (people[i].PersonCars.ElementAt(j).DateTo.CompareTo(DateTime.Now) >= 0))
            //            {
            //                if (people[i].PersonCars.ElementAt(j).Car.Id == id)
            //                {
            //                    person = people[i];

            //                    if (newOwner != null)
            //                    {
            //                        person.PersonCars.ElementAt(j).DateTo = changeCarOwnerViewOwner.DateTime;
            //                    }
            //                    goto end;
            //                }

            //            }

            //        }

            //    }

            //end:

            if (newOwner != null)
            {
                if (car!=null)
                {


                    newOwner.PersonCars.Add(new PersonCar
                    {
                        Car = car,
                        CarId = car.Id,
                        DateFrom = changeCarOwnerViewOwner.Date.AddMinutes(1),
                        DateTo = DateTime.Now.AddYears(10),
                        Person = newOwner,
                        PersonId = newOwner.Id,
                    });
                }
            }
            if ((oldPerson != null) && (newOwner != null))
            {
                Database.People.Update(oldPerson);
                Database.People.Update(newOwner);
            }
            await Database.SaveChangesAsync();



        }

        public async Task AddFine(long id, FinePersonInputData finePersonInputData)
        {
            var person = await Database.People.Include(w => w.Fines).Include(a => a.PersonCars).FirstOrDefaultAsync(i => i.Id == id);
            var number = finePersonInputData.CarNumber;
            var correctRegisterNumber = string.Empty;
            var correctNumber = string.Empty;
            if (finePersonInputData.NumberRegistrator.StartsWith('\t'))
            {
                finePersonInputData.NumberRegistrator.Remove(0, 1);
            }
            else
            {
                correctRegisterNumber = finePersonInputData.NumberRegistrator;
            }
            if (finePersonInputData.CarNumber.StartsWith('\t'))
            {
                correctNumber = finePersonInputData.CarNumber.Remove(0, 1);
            }
            else
            {
                correctNumber = finePersonInputData.CarNumber;
            }


            var car = await Database.Cars.Where(i => i.Number == correctNumber).FirstOrDefaultAsync();
            Registrator registrator = await Database.Registrators.FirstOrDefaultAsync(i => i.GerNumber == correctRegisterNumber);
            if (registrator == null)
            {
                registrator = new Registrator { GerNumber = correctRegisterNumber };
            }
            if (car != null)
            {
                Fine fine = new()
                {
                    Value = finePersonInputData.Value,
                    IsActive = true,
                    DateTmeOfAccident = finePersonInputData.Date,
                    Car = car,
                    Driver = person,
                    Registrator = registrator

                };
                car.Fines.Add(fine);
                Database.Update(car);  
                person.Fines.Add(fine);
            }
          
            Database.Update(person);
            await Database.SaveChangesAsync();

        }
        public async Task RemoveCar(long carId)
        {
            var car = await Database.Cars.FirstOrDefaultAsync(i => i.Id == carId);
            if (car != null)
            {
                Database.Cars.Remove(car);

            }
            await Database.SaveChangesAsync();
        }

        public async Task ChangeOwnerCar(long PersonId, long CarId, ChangeCarOwnerData changeCarOwnerViewOwner)
        {
            var person = await Database.People.FirstOrDefaultAsync(i => i.Id == PersonId);
            if (person != null)
            {
                for (int i = 0; i < person.PersonCars.Count; i++)
                {
                    if (person.PersonCars.ElementAt(i).Car.Id == CarId)
                    {
                        person.PersonCars.ElementAt(i).DateTo = changeCarOwnerViewOwner.Date;
                        var newOwner = await Database.People.FirstOrDefaultAsync(a => a.Id == changeCarOwnerViewOwner.Id);
                        if (newOwner != null)
                        {

                        }
                    }
                }
            }
        }
        public async Task AddFine(long id, FineCarInputData fineCarInputData)
        {
            var car = await Database.Cars.FirstOrDefaultAsync(i => i.Id == id);
            var personCar = await Database.PersonCars.Where(i => i.Car == car).FirstOrDefaultAsync();
            var registrator = await Database.Registrators.FirstOrDefaultAsync(i => i.GerNumber == fineCarInputData.CreatedBy);
            if (registrator == null)
            {
                registrator = new Registrator()
                {
                    GerNumber = fineCarInputData.CreatedBy
                };
            }
            Person owner = new Person();
            if (personCar != null)
            {
                owner = personCar.Person;
            }
            if (car != null)
            {
                car.Fines.Add(new Fine
                {

                    IsActive = true,
                    DateTmeOfAccident = fineCarInputData.DateTime,
                    Value = fineCarInputData.Value,
                    Driver = personCar?.Person,
                    Registrator = registrator

                });
            }
            Database.Cars.Update(car);
            Database.People.Update(owner);
            await Database.SaveChangesAsync();
        }
        public async Task<PersonDataOutput[]> Search(Search searchValue)
        {
            var value = searchValue.Data;
            var persons = await Database.People.Where(a => a.Surname.Contains(value)).Include(z=>z.Fines).ToListAsync();

            _ = long.TryParse(value, out long idValue);
            
            var personsById = await Database.People.Where(i => i.Id == idValue).ToListAsync();
            var cars = await Database.Cars.Where(a => a.Number == value).ToListAsync();
            if (persons.Count != 0)
            {
                List<PersonDataOutput> personDataOutputs = new List<PersonDataOutput>();
                for(int i=0;i<persons.Count;i++)
                {
                    var dataP =await GetPersonFineAndCarData(persons[i].Id);
                    personDataOutputs.Add(new PersonDataOutput
                    {
                        Id = persons[i].Id,
                        City = persons[i].City,
                        Address=persons[i].Address,
                        Surname=persons[i].Surname,
                        CarData=dataP.carData,
                        FineData=dataP.fineData

                    }) ;
                }
                return personDataOutputs.ToArray();

            }
            //if(personsById.Count!=0)
            //{
            //    return personsById.ToArray();
            //}
                
            //if (cars.Count != 0)
            //{
            //    return cars.ToArray();
            //}
            return null;

        }
        public async Task<(string fineData,string carData)> GetPersonFineAndCarData(long id)
        {
            var person = await Database.People.Include(q => q.Fines).Include(s => s.PersonCars).FirstOrDefaultAsync(a => a.Id == id);
            var cars = await Database.Cars.Include(i => i.PersonCars).Include(a => a.Fines).ToListAsync();

            int currentCarCount = 0;
            int finesCount = 0;
            int activeFines = 0;
            var fines = 0;
            var actFines = 0;
            for (int j = 0; j < person.PersonCars.Count; j++)
            {
                var begin = person.PersonCars.ElementAt(j).DateFrom;
                var end = person.PersonCars.ElementAt(j).DateTo;
                var fineInfo = await Database.Fines.Where(a => (DateTime.Compare(begin, a.DateTmeOfAccident) <= 0) && (DateTime.Compare(end, a.DateTmeOfAccident) >= 0) &&(a.Car.Id==person.PersonCars.ElementAt(j).CarId)).ToListAsync();

                var allFinesCount = fineInfo.Count;
                var activeFinesCount = fineInfo.Where(cond => cond.IsActive).Count();
                fines = allFinesCount;
                actFines = activeFinesCount;
                var now = DateTime.Now;
                int fisrt_check = DateTime.Compare(begin, now);
                int second_check = DateTime.Compare(end, now);
                if ((fisrt_check <= 0) && (second_check >= 0))
                {
                    currentCarCount++;
                    var s = cars.FirstOrDefault(qq => qq.Id == person.PersonCars.ElementAt(j).CarId);
                    for (int z = 0; z < s.Fines.Count; z++)
                    {
                        var firstFinesCheck = DateTime.Compare(begin, s.Fines.ElementAt(z).DateTmeOfAccident);
                        var sedondFinesCheck = DateTime.Compare(end, s.Fines.ElementAt(z).DateTmeOfAccident);
                        if ((firstFinesCheck <= 0) && (sedondFinesCheck >= 0))
                        {
                            finesCount++;
                            if (s.Fines.ElementAt(z).IsActive)
                            {
                                activeFines++;
                            }
                        }
                    }
                    //  finesCount += s.Fines.Count;
                    // finesCount += all[i].PersonCars.ElementAt(j).Car.Fines.Count();
                    //activeFines+=all[i].PersonCars.ElementAt(j).Car.Fines.Where(q => q.IsActive).Count();
                    //activeFines += (s.Fines.Where(act => act.IsActive).Count());
                    //all[i].Fines=   all[i].Fines.Union(all[i].PersonCars.ElementAt(j).Car.Fines);
                }
                else
                {
                    var carInfo = cars.FirstOrDefault(c => c.Id == person.PersonCars.ElementAt(j).CarId);
                    carInfo.Fines.Select(a => a.DateTmeOfAccident);
                    var finesCounts = person.Fines.Count;
                    for (int abc = 0; abc < finesCounts; abc++)
                    {
                        var checkBegin = DateTime.Compare(person.PersonCars.ElementAt(j).DateFrom, person.Fines.ElementAt(abc).DateTmeOfAccident);
                        var checkEnd = DateTime.Compare(person.PersonCars.ElementAt(j).DateTo, person.Fines.ElementAt(abc).DateTmeOfAccident);
                        if ((checkBegin <= 0) && (checkEnd >= 0))
                        {
                            finesCount++;
                            if (person.Fines.ElementAt(abc).IsActive)
                            {
                                activeFines++;
                            }
                        }


                    }
                }
            }
            int activeCount = person.Fines.Where(i => i.IsActive).Count();
            var kekw = await GetFinesByCar((long)(person.PersonCars.LastOrDefault()?.CarId));
            string fineData = $"{activeFines}/{fines}";
            string carData = $"{currentCarCount}/{person.PersonCars.Count}";
            return await Task.FromResult((kekw, carData));
           

        }

        public async Task<string> GetFines(long id)
        {
            var person = await Database.People.Include(s=>s.PersonCars).Include(q=>q.Fines).FirstOrDefaultAsync(i => i.Id == id);
            var allFineCount = 0;
            var activeFineCount = 0;

            if (person != null)
            {
                if(person.PersonCars!=null)
                {
                    for(int i=0;i<person.PersonCars.Count;i++)
                    {
                        var fines = await Database.Fines.Where(a => (a.DateTmeOfAccident.CompareTo(person.PersonCars.ElementAt(i).DateFrom) >= 0)
                          && (a.DateTmeOfAccident.CompareTo(person.PersonCars.ElementAt(i).DateTo) <= 0)
                        ).ToListAsync();
                        allFineCount += fines.Count;
                        activeFineCount += (fines.Where(z => z.IsActive)).Count();
                    }
                    return await Task.FromResult($"{activeFineCount}/{allFineCount}");
                }
            
             //   person.PersonCars.Where(a => a.)
            }
             return null;
        }
        public async Task<Dictionary<long, string>> GetFineByCars()

        {
            var cars = await Database.Cars.ToListAsync();
            Dictionary<long, string> finesCarsId = new();
            for(int i=0;i<cars?.Count;i++)
            {
                var fines =await Database.Fines.Where(f => f.CarId == cars[i].Id).ToListAsync();
                var allCount = fines.Count;
                var activeFines = fines.Where(z => z.IsActive).Count();
                finesCarsId.Add(cars[i].Id, $"{activeFines}/{allCount}");
            }
            return await Task.FromResult(finesCarsId);
        }
        public async Task<string> GetFinesByCar(long id)
        {
            string result = string.Empty;
            var car = await Database.Cars.Include(a => a.PersonCars).Include(f => f.Fines).Where(i => i.Id == id).FirstOrDefaultAsync();
            if(car!=null)
            {
                var fines = await Database.Fines.Where(f => f.CarId == id).ToListAsync();
                var allCount = fines.Count;
                var activeFines = fines.Where(z => z.IsActive).Count();
              result= $"{activeFines}/{allCount}";

            }
            return await Task.FromResult(result);
            
        }
        public async Task<List<FineCarInfo>> GetFineCarInfoById(long id )
        {
            var car = await Database.Cars.Include(a => a.PersonCars).Include(f => f.Fines).Where(i => i.Id == id).FirstOrDefaultAsync();
            List<FineCarInfo> res = new List<FineCarInfo>();
            if (car != null)
            {
                var fines = await Database.Fines.Where(a => a.CarId.Value == car.Id).ToListAsync();
              
                var peopleOwners = await Database.People.Where(p => p.PersonCars.Any(z => z.CarId == car.Id)).ToListAsync();

             var driverIds=   fines.Select(s => s.DriverId).ToList();
                var people = await Database.People.Where(ss => driverIds.Contains(ss.Id)).ToListAsync();

                for(int i=0;i<people.Count;i++)
                {
                   

                        var finesBy = fines.Where(a => (a.DateTmeOfAccident.CompareTo(people[i].PersonCars.Where(q=>q.CarId==id).FirstOrDefault()?.DateFrom)) >= 0
                   && (a.DateTmeOfAccident.CompareTo(people[i].PersonCars.Where(t=>t.CarId==id).FirstOrDefault()?.DateTo) <= 0));
                    for (int f = 0; f < finesBy.Count(); f++)
                    {

                        FineCarInfo fineCarInfo = new FineCarInfo
                        {
                            City = people[i].City,
                            Name = people[i].Surname,
                            DateTimeAccident=finesBy.ElementAt(f).DateTmeOfAccident,
                            IsActive=finesBy.ElementAt(f).IsActive,
                            Id=people[i].Id,
                            Value=finesBy.ElementAt(f).Value

                        };
                        res.Add(fineCarInfo);
                    }
                }

                for (int i = 0; i < peopleOwners.Count; i++)
                {
                    for(int g=0;g<peopleOwners[i].PersonCars.Count;g++)
                    {

                    }
                   var finesBy= fines.Where(a => (a.DateTmeOfAccident.CompareTo(car.PersonCars.ElementAt(i).DateFrom)) >= 0
                    && (a.DateTmeOfAccident.CompareTo(car.PersonCars.ElementAt(i).DateTo) <= 0));
                    
                       
                }
                

            }

            return res;
            
        }


    }
}
