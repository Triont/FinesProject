using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Model;
using Microsoft.EntityFrameworkCore;


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
            return await People.FirstOrDefaultAsync(i => i.Id == id);
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
        public async Task Edit(Person person)
        {
            Database.People.Update(person);
            await Database.SaveChangesAsync();
        }
        public async Task<List<Person>> GetPeopleAsync()
        {
           return await Database.People.ToListAsync();
        }
    }
}
