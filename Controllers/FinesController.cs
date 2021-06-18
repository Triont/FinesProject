using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Model;
using Project5.Models;
using Project5.Services;


namespace Project5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinesController : ControllerBase
    {

        private readonly PersonService personService;

        public FinesController(PersonService personService)
        {
            this.personService = personService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(long id, [FromBody] Fine fine)
        {

            return Ok();

        }

    }
}
