using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Model;
using Project5.Models;
using Project5.Services;

namespace Project5.Services
{
    public class CarService
    {

       private readonly CarRepo car;
        public CarService(CarRepo car)
        {
            this.car = car;

        }
        public async Task<IEnumerable<CarShowViewModel>> GetAll()
        {
           return  await this.car.GetAll();
        }
        public async Task AddFine(long id, FinesInputViewModel finesInputViewModel)
        {
            await this.car.AddFine(id, finesInputViewModel);
        }
        public async Task<FineByCarInfo[]> GetInfo(long id)
        {
            return await this.car.GetFineInfo(id);
        }
    }
}
