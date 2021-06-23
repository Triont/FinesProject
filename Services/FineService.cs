using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Model;
using Project5.Models;
using Project5.Services;

namespace Project5.Services
{
    public class FineService
    {
        private readonly FineRepo fineRepo;
        public FineService(FineRepo fineRepo)
        {
            this.fineRepo = fineRepo;
        }
        public async Task CloseFine(long id)
        {
            await this.fineRepo.CloseFine(id);
        }
    }
}
