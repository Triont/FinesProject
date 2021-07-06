using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Models;
using Project5.Model;

using Microsoft.EntityFrameworkCore;

namespace Project5.Services
{
    public class PersonService:IPersonService
    {
         readonly PersonRepo person;
        private readonly IPersonRepo personRepo;
        public PersonService(IPersonRepo personRepo, PersonRepo person)
        {
             this.person = person;
            this.personRepo = personRepo;
   
            
        }
        public IQueryable<Person> GetPersonRepo()
        {
            
            //ClassLibrary1.Repositories.Class1 class1 = new ClassLibrary1.Repositories.Class1();
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
            return await this.person.GetPersonsDataFromLibrary();
           // return  await this.person.GetPersonsData();
          //  return await this.person.GetFromLibrary();
            
            
            //Repositories.Class1 class1 = Repositories.Class1();
            //var t=await class1.GetPersonsData();
           // return t;
            //  return await this.person.GetAllPersons();
        }

        public async Task CreatePerson(Person person)
        {
            
            await this.person.CreatePerson(person);
        }
        public async Task<PersonCarFineDataOutput>  Get(long id)
        {
         return   await this.person.GetPersonData(id);
          // return await this.person.GetPerson(id);
        }
        public async Task Edit(Person person)
        {
            await this.person.Edit(person);
        }
        public async Task Edit(PersonUpdateData person)
        {
            await this.person.Edit(person);
        }
        public async Task Delete(Person person)
        {
            await this.person.DeletePerson(person);
        }

        public async Task Delete(long id)
        {
            await this.person.DeletePerson(id);
        }
        public async Task AddCar(long id, CarDataInput car)
        {
            await this.person.AddCar(id, car);
        }
        public async Task AddFine(long id, FinePersonInputData fineInputData)
        {
            await this.person.AddFine(id, fineInputData);

        }
        public async Task AddFine(long id, FinesInputViewModel finesInputViewModel)
        {
            await this.person.CreateFine(id, finesInputViewModel);
        }
        public async Task AddFine(long id, FineCarInputData fineCarInputData)
        {
            await this.person.AddFine(id, fineCarInputData);

        }
        public async Task ChangeOwner(long id, ChangeCarOwnerData changeCarOwnerViewOwner)
        {
            await this.person.ChangeOwner(id, changeCarOwnerViewOwner);
        }


        public async Task<PersonDataOutput[]> Search(Search value)
        {
            return  await  this.person.Search(value);
        }

       
    }

    public interface IPersonService
    {
        Task AddFine(long id, FinesInputViewModel finesInputViewModel);
        Task AddCar(long id, CarDataInput carDataInput);
        Task Delete(long id);
        Task Edit(Person person);
        Task<PersonCarFineDataOutput> Get(long id);
        Task<IEnumerable<PersonDataOutput>> GetAllPerson();
    }
}
