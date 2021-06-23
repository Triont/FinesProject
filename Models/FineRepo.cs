using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Models;
using Project5.Model;
using Project5.Services;
using Microsoft.EntityFrameworkCore;

namespace Project5.Models
{
    public class FineRepo
    {
        private readonly Database_FinesContext FinesContext;
        public FineRepo(Database_FinesContext database_FinesContext)
        {
            this.FinesContext = database_FinesContext;
        }
        public async Task CloseFine(long id)
        {
            var fine = await FinesContext.Fines.FirstOrDefaultAsync(i => i.Id == id);
            if(fine!=null)
            {
                if(fine.IsActive)
                {
                    fine.IsActive = false;
                }
                FinesContext.Fines.Update(fine);
                await FinesContext.SaveChangesAsync();
            }
        }
    }
}
