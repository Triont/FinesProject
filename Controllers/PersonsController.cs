using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project5.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project5.Models;
using System.Threading.Tasks;
using Project5.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService personService;
        public PersonsController( PersonService personService)
        {
            
            this.personService = personService;
        }
        // GET: api/<PersonsController>
        [HttpGet]
        public async Task< PersonDataOutput[]> Get()
        {
            //if ((await database_FinesContext.People.ToListAsync()).Count == 2)
            //{
            //    WriteData();
            //}

            //var people=await database_FinesContext.People.ToListAsync();


         
            //    await      personService.CreatePerson(new Person()
            //{

            //    Surname="NewName", Address="Some street", City="Kha", Id=3, PersonCars=new List<PersonCar>() { new PersonCar() { Car=new Car() { Name="Ford",
            //    Number="13ffa3" }
            //    } }
            //});
            
           //    var serviceResultNew = await personService.GetAll();
            var serviceResWithFines = await personService.GetAllPerson();
            //var r = serviceResWithFines.ToList();


            //   var result= database_FinesContext.People.FromSqlRaw("SELECT * from Person");
            // var results = database_FinesContext.People.FromSqlRaw("SELECT *, (Select * from PersonCars Where PersonCars.PersonId=Person.Id ).Count() from Person    ");
            //  var r =await result.ToListAsync();

            // var nnn = await results.ToListAsync();
            //   return serviceResultNew.ToArray();
            return serviceResWithFines.ToArray();
          //  return r.ToArray();
      //      return new string[] { "value1", "value2" };
        }



        public async void WriteData()
        {
            Person person = new Person
            {
                Surname = "fa",
                City = "NewTown",
                Address = "avenue",
                Id=2,
                PersonCars = new
                List<PersonCar> { new PersonCar() { Car = new Car() { Name = "Ford", Number = "23fsacb" } , DateFrom=DateTime.Now, DateTo=
                    DateTime.Now.AddDays(356)
                }
                }
            };
            //await database_FinesContext.People.AddAsync(new Person()
            // {
            //     Surname = "fa",
            //     City = "NewTown",
            //     Address = "avenue",
            //    PersonCars = new
            //     List<PersonCar> { new PersonCar() { Car = new Car() { Name = "Ford", Number = "23fsacb" } }

            // });
            //await database_FinesContext.People.AddAsync(person);
            //await database_FinesContext.SaveChangesAsync();

        }

        // GET api/<PersonsController>/5
        [HttpGet("{id}")]
        public async Task<PersonCarFineDataOutput> Get(int id)
        {

            var person =await this.personService.Get(id);
            return person;


        
        }

        // POST api/<PersonsController>
        //[HttpPost]
        //public void Post([FromBody] string value, int id)
        //{

            

        //}

        [HttpPost]
        public async Task Post(Person  json)
        {
        //   var tmp= JsonConvert.DeserializeObject<Person>(json);
           await this.personService.CreatePerson(json);
        }

        // PUT api/<PersonsController>/5
        [HttpPut("{id}")]
        public async Task Put(long id, [FromBody] PersonUpdateData person)
        {
            await this.personService.Edit(person);
        }

        // DELETE api/<PersonsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
           await personService.Delete(id);
        }
    }
}
