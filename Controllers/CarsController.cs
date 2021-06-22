using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Project5.Model;
using Microsoft.EntityFrameworkCore;
using Project5.Models;
using Project5.Services;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CarsController : ControllerBase
    {

    

        private readonly PersonService personService;
        private Logger<CarsController> logger;
        public CarsController( PersonService personService, Logger<CarsController> logger)
        {
          
            this.personService = personService;
            this.logger = logger;
            this.logger.LogInformation("In constructor");
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
            //var result = await db_context.PersonCars.Where(i => i.PersonId == id).ToListAsync();
            //var _res = new List<Car>();
            //for (int i = 0; i < result.Count; i++)
            //{
            //    var cars = await db_context.Cars.Where(it => it.Id == result[i].CarId).ToListAsync();
            //}
            this.logger.LogInformation("In get method");
            throw new Exception();
        }

        // POST api/<CarsController>
       // [HttpPost("CreateCar/{id}")]
       //// [Route("createCar")]
       // public async Task Post(long id, CarDataInput value)
       // {




       //     await personService.AddCar(id, value);

       // }
        [HttpPost("{id}")]
        public async Task Post(long id, ChangeCarOwnerData changeCarOwnerViewOwner)
        {
            this.logger.LogInformation("In post method");
            await this.personService.ChangeOwner(id, changeCarOwnerViewOwner);
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
