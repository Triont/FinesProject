using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Model;
using Project5.Models;
using Microsoft.EntityFrameworkCore;

namespace Project5.Models
{
    public class CarRepo
    {
        private readonly Database_FinesContext finesContext;
        public CarRepo(Database_FinesContext finesContext)
        {
            this.finesContext = finesContext;
        }

        public async Task<IEnumerable<CarShowViewModel>> GetAll()
        {
            var cars = await finesContext.Cars.Include(w=>w.Fines).ToListAsync();
            List<CarShowViewModel> carShowViewModels = new List<CarShowViewModel>();
            for(int i=0;i<cars.Count;i++)
            {
                var activeFines = cars[i].Fines.Where(x => x.IsActive == true).Count();
                var allFines = cars[i].Fines.Count;
                //var personCar = await finesContext.PersonCars.Where(pp =>( pp.Car == cars[i])&&((pp.DateFrom.CompareTo(DateTime.Now)<=0) 
                //&&(pp.DateTo.CompareTo(DateTime.Now)>=0)
                //) ).FirstOrDefaultAsync();
                //long ownerId = 0;
                //if(personCar!=null)
                //{
                //    ownerId = personCar.PersonId;
                //}
               
                CarShowViewModel carShowViewModel = new CarShowViewModel
                {
                    Id = cars[i].Id,
                    Name = cars[i].Name,
                    Number = cars[i].Number,
                    Fines=$"{activeFines}/{allFines}",
                  
                    
                };
                carShowViewModels.Add(carShowViewModel);
            }
            return carShowViewModels;
        }
        public async Task AddFine(long id, FinesInputViewModel fineCarInputData)
        {
            var car = await finesContext.Cars.Include(f=>f.Fines).FirstOrDefaultAsync(i => i.Id == id);
            if(car!=null)
            {
                var currentOwner = await finesContext.PersonCars.Include(f=>f.Person).Include(c=>c.Car).Where(a => a.DateFrom
                .CompareTo(DateTime.Now) <= 0 && a.DateTo.CompareTo(DateTime.Now) >= 0).FirstOrDefaultAsync();
                if(currentOwner!=null)
                {
                    var registrator = await finesContext.Registrators.FirstOrDefaultAsync(z => z.GerNumber == fineCarInputData.NumberRegistrator);
                    if(registrator==null)
                    {
                        registrator = new Registrator
                        {
                            GerNumber = fineCarInputData.NumberRegistrator
                        };
                    }
                    currentOwner.Person.Fines?.Add(new Fine
                    {
                        DateTmeOfAccident=fineCarInputData.DateTime,
                        Driver=currentOwner.Person,
                        DriverId=currentOwner.PersonId,
                        Car=car,
                        CarId=car.Id,
                        IsActive=true,
                        IsPersonal=fineCarInputData.IsPersonal,
                        Registrator=registrator,
                        
                        Value=fineCarInputData.Value

                    });
                    finesContext.PersonCars.Update(currentOwner);
                }

            }
            await finesContext.SaveChangesAsync();
        }
        public async Task<FineByCarInfo[]> GetFineInfo(long id)
        {
            var car = await finesContext.Cars.FirstOrDefaultAsync(i => i.Id == id);
            if(car!=null)
            {
                var fines = car.Fines.ToList();
                List<FineByCarInfo> fineByCarInfos = new List<FineByCarInfo>();
                for (int i = 0; i < fines.Count; i++)
                {
                    var city = (await finesContext.People.Where(a => a.Id == fines[i].DriverId).FirstOrDefaultAsync()).City;
                    FineByCarInfo fineByCarInfo = new FineByCarInfo
                    {
                        DateTimeAccident = fines[i].DateTmeOfAccident,
                        IsActive = fines[i].IsActive,
                        Value = fines[i].Value,
                        Id = fines[i].Id,
                        City = city


                    };
                    fineByCarInfos.Add(fineByCarInfo);

                }
                return fineByCarInfos.ToArray();
            }
            return null;


        }

    }
}
