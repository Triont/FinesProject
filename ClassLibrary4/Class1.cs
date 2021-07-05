using System;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ClassLibrary4.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ClassLibrary4
{
    public class Class1
    {
    }
    public class PersonRepo
    {

    }
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        Task SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly Database_FinesContext _dbContext;
        private Hashtable _repositories;
        public UnitOfWork(Database_FinesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
    public class BaseEntity
        {

            public long Id { get; set; }
        }
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task InsertEntityAsync(TEntity entity);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly Database_FinesContext _dbContext;
        public Repository(Database_FinesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertEntityAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }
        public async Task<TEntity> GetEntityAsync(TEntity entity, long id)
        {

         var result=await   _dbContext.Set<TEntity>().FirstOrDefaultAsync(q => q.Id == id);
            if(result!=null)
            {
                return result;
            }
            return null;
        }
        public async Task<List<Person>> GetAll(TEntity entity)
        {
            var result = await _dbContext.People.ToListAsync();
            return result;
        }
        public async Task AddFine(long id)
        {
            var person = await _dbContext.People.Include(f=>f.Fines).FirstOrDefaultAsync(a => a.Id == id);
            if(person!=null)
            {
                person.Fines?.Add(new Fine
                {

                });
            }
        }
    }
    public static class ServiceCollectionExtensions
    {

        public static void RegisterYourLibrary(this IServiceCollection services, Database_FinesContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>(uow => new UnitOfWork(dbContext));
        }
    }
}
