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
        public async Task<List<Person>> GetPeopleAsync()
        {
           return await Database.People.ToListAsync();
        }
        public async Task<IEnumerable<PersonDataOutput>> GetAllPersons()
        {
            var all = await GetPeopleAsync();
            List<PersonDataOutput> outputs = new List<PersonDataOutput>();
            for(int i=0;i<all.Count;i++)
            {
                PersonDataOutput personDataOutput = new PersonDataOutput();
                personDataOutput.Person = all[i];
                var count = all[i].Fines.Count;
                personDataOutput.FineData = count.ToString();
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


       
    }

}
