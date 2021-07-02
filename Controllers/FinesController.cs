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
        private readonly FineService fineService;

        public FinesController(PersonService personService, FineService fineService)
        {
            this.personService = personService;
            this.fineService = fineService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(long id, [FromBody] FinePersonInputData fine)
        {
            await this.personService.AddFine(id, fine);
            return Ok();

        }
        [HttpPost("TestPost/{id}")]
        public async Task<IActionResult> TestPost(long id, FinesInputViewModel finesInputViewModel)
        {
            await this.personService.AddFine(id, finesInputViewModel);
            return Ok();
        }

        //[HttpPost("{id}")]
        //public async Task<IActionResult> Post(long id, [FromBody] FineCarInputData fine)
        //{
        //    await this.personService.AddFine(id, fine);
        //    return Ok();

        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id)
        {

            await this.fineService.CloseFine(id);
            return Ok();
        }


    }
}
