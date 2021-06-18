using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Project5.Model;
using Microsoft.EntityFrameworkCore;
using Project5.Models;
using Project5.Services;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly Database_FinesContext db_context;

        private readonly PersonService personService;
        public CarsController(Database_FinesContext database_FinesContext, PersonService personService)
        {
            this.db_context = database_FinesContext;
            this.personService = personService;
        }
        // GET: api/<CarsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        public async Task<Car[]> Get(long id)
        {
            var result = await db_context.PersonCars.Where(i => i.PersonId == id).ToListAsync();
            var _res = new List<Car>();
            for (int i = 0; i < result.Count; i++)
            {
                var cars = await db_context.Cars.Where(it => it.Id == result[i].CarId).ToListAsync();
            }

            throw new Exception();
        }

        // POST api/<CarsController>
        [HttpPost("{id}")]
        public async Task Post(long id, CarDataInput value)
        {




            await personService.AddCar(id, value);

        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public async Task Put(long id, [FromBody] CarDataInput value)
        {
            await personService.AddCar(id, value);
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
