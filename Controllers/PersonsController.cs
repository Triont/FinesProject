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
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService personService;
      private readonly IPersonService person;
       private readonly ServicesLibs.IPersonService personService1;
        private readonly IMapper mapper;
        public PersonsController( PersonService personService,
                                 IPersonService service,ServicesLibs.IPersonService personService1, 
            
            IMapper mapper)
        {
            
           this.personService = personService;
        this.person = service;
           this.personService1 = personService1;
            this.mapper = mapper;
        }
        // GET: api/<PersonsController>
        [HttpGet]
        public async Task< PersonDataOutput[]> Get()
        {
       
            var serviceResWithFines = await person.GetAllPerson();
         
            return serviceResWithFines.ToArray();
 
        }

        [HttpGet("GetFromLib")]
        public async Task<PersonDataOutput[]> GetFromLib()
        {
            var result = await personService1.GetPersonDataOutputs();
            List<PersonDataOutput> personDataOutputs = new List<PersonDataOutput>();
            for (int i = 0; i < result.Length; i++)
            {
                personDataOutputs.Add(mapper.Map<PersonDataOutput>(result[i]));
            }
            return personDataOutputs.ToArray();
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
        [HttpPost("search")]
        public async Task<PersonDataOutput[]> Search(Search searchData)
        {
            var result = await this.personService.Search(searchData);
            return result;
        }
   }
}
