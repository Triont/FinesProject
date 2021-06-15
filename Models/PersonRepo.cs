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
       
        public async Task<Person> GetPerson(long id)
        {
            var r= await Database.People.FirstOrDefaultAsync(i => i.Id == id);
            return r;
        
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
                                ,
                            }
                        },
                         Number="13fffa"
                    },


                    }


                    }
                   
                });
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


       
    }

}
