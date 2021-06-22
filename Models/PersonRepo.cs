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
            var r= await Database.People.Include(a=>a.PersonCars).ThenInclude(f=>f.Car).FirstOrDefaultAsync(i => i.Id == id);
           
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
            for(int i=0;i<a.Length;i++)
            {
                FineDataOutput fineDataOutput = new FineDataOutput();
                fineDataOutput.Value = a[i].Value;
                fineDataOutput.IsActive = a[i].IsActive;
                fineDataOutput.Id = a[i].Id;
                fineDataOutput.CarId = a[i].CarId;
                finesData.Add(fineDataOutput);
            }
            List<CarDataOutput> carDatas = new List<CarDataOutput>();

            for(int i=0;i<arr.Length;i++)
            {
                CarDataOutput carDataOutput = new CarDataOutput();
                carDataOutput.Id = arr[i].Id;
                carDataOutput.Name = arr[i].Name;
                carDataOutput.Number = arr[i].Number;
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
        public  async Task DeletePerson(Person person)
        {
            Database.People.Remove(person);
            await Database.SaveChangesAsync();
        }

        public async Task DeletePerson(int id)
        {
           var toDelete=await Database.People.FirstOrDefaultAsync(i => i.Id == id);
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
           var personToEdit= await Database.People.FirstOrDefaultAsync(i => i.Id == person.Id);
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

            if(Database.People.Count()==0)
            {
              await  Database.People.AddAsync(new Person()
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
            for(int i=0;i<all.Count;i++)
            {
                PersonDataOutput personDataOutput = new PersonDataOutput();
              //  personDataOutput.Person = all[i];
                personDataOutput.Id = all[i].Id;
                personDataOutput.Surname = all[i].Surname;
                personDataOutput.City = all[i].City;
                personDataOutput.Address = all[i].Address;
                var count = all[i].Fines.Count();

                var allCarsCount = all[i].PersonCars.Count();
                int currentCarCount = 0;
                var finesCount = 0;
                var activeFines = 0;
              for(int j=0;j<allCarsCount;j++)
                {
                   var begin= all[i].PersonCars.ElementAt(j).DateFrom;
                    var end = all[i].PersonCars.ElementAt(j).DateTo;
                  
                    var now = DateTime.Now;
                    int fisrt_check = DateTime.Compare(begin, now);
                    int second_check = DateTime.Compare(end, now);
                    if((fisrt_check<=0) &&(second_check>=0))
                    {
                        currentCarCount++;
                    var s=    cars.FirstOrDefault(qq => qq.Id == all[i].PersonCars.ElementAt(j).CarId);
                       finesCount+= s.Fines.Count;
                        // finesCount += all[i].PersonCars.ElementAt(j).Car.Fines.Count();
                        //activeFines+=all[i].PersonCars.ElementAt(j).Car.Fines.Where(q => q.IsActive).Count();
                        activeFines += (s.Fines.Where(act => act.IsActive).Count());
                     //all[i].Fines=   all[i].Fines.Union(all[i].PersonCars.ElementAt(j).Car.Fines);
                    }
                }
                int activeCount=all[i].Fines.Where(i => i.IsActive).Count();
               
                personDataOutput.FineData = $"{activeFines}/{finesCount}";
                personDataOutput.CarData = $"{currentCarCount}/{allCarsCount}";
                outputs.Add(personDataOutput);
                
            }
            return  outputs;
          
        }
        public async Task<List<object>> GeAllPeopleAsync()
        {
            var tmp = await Database.People.ToListAsync();
            for(int i=0;i<tmp.Count;i++)
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
            var person = await Database.People.Include(a=>a.PersonCars).Include(q=>q.Fines).FirstOrDefaultAsync(i => i.Id == id);
            if (person != null)
            {
                person.PersonCars.Add(new PersonCar()
                {
                    Car = new Car() { Name = car.name, Number = car.number },
                    DateFrom = car.date,
                    DateTo = car.date.AddDays(3650)
                }

                    ) ;
            }
            Database.People.Update(person);
           await Database.SaveChangesAsync();
        }

        public async Task ChangeOwner(long id, ChangeCarOwnerViewOwner changeCarOwnerViewOwner)
        {
                var car=await  Database.Cars.FirstOrDefaultAsync(i => i.Id == id);

           var people=await  Database.People.ToListAsync();
            Person person = new Person();
            for(int i=0; i<people.Count;i++)
            {


                for(int j=0;j<people[i].PersonCars.Count;j++)
                {

                }


             if(  people[i].PersonCars.FirstOrDefault(a => a.Car == car)!=null)
                {
                  
                    person = people[i];
                }
            }
        }

        public async Task AddFine(long id, FinePersonInputData finePersonInputData)
        {
            var person = await Database.People.FirstOrDefaultAsync(i => i.Id == id);
            Fine fine = new Fine { };
        }


       
    }

}
