using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassLibrary1.Data;

namespace ClassLibrary1.Repositories
{
    class Class1
    {

        public async Task<Person> Get()
        {
            using (Data.Database_FinesContext database_FinesContext = new Database_FinesContext())
            {
             return await   database_FinesContext.People.ToListAsync();
            }
        }
    }
}
