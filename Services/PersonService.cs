using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Model;
using Microsoft.EntityFrameworkCore;

namespace Project5.Services
{
    public class PersonService
    {
        public readonly PersonRepo person;
        public PersonService(PersonRepo person)
        {
            this.person = person;
            
        }
        

    }
}
