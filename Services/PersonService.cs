using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Models;
using Project5.Model;
using Microsoft.EntityFrameworkCore;

namespace Project5.Services
{
    public class PersonService
    {
         readonly PersonRepo person;
        public PersonService(PersonRepo person)
        {
            this.person = person;
            
        }
        public IQueryable<Person> GetPersonRepo()
        {
          
            return person.People;
        }
        public  PersonRepo GetRepo()
        {
            return this.person;
        }
        public async Task<List<Person>> GetAll()
        {

            return await this.person.GetPeopleAsync();
        }
        public async Task<IEnumerable<PersonDataOutput>> GetAllPerson()
        {
            return await this.person.GetAllPersons();
        }

        public async Task CreatePerson(Person person)
        {
            await this.person.CreatePerson(person);
        }
        public async Task<Person>  Get(long id)
        {
           return await this.person.GetPerson(id);
        }
        public async Task Edit(Person person)
        {
            await this.person.Edit(person);
        }
        public async Task Delete(Person person)
        {
            await this.person.DeletePerson(person);
        }

        public async Task Delete(int id)
        {
            await this.person.DeletePerson(id);
        }
        

    }
}
