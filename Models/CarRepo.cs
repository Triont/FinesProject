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
                CarShowViewModel carShowViewModel = new CarShowViewModel
                {
                    Id = cars[i].Id,
                    Name = cars[i].Name,
                    Number = cars[i].Number,
                    Fines=$"{activeFines}/{allFines}"
                };
            }
            return carShowViewModels;
        }

    }
}
