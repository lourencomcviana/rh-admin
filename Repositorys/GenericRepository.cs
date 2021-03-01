using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rh_admin.Models;

namespace rh_admin.Repositorys
{
    public class GenericGenericRepository<TDbContext> :IGenericRepository 
        where TDbContext : DbContext 
    {
        private readonly TDbContext _dbContext;

        public GenericGenericRepository(TDbContext context)
        {
            _dbContext = context;
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            this._dbContext.Set<T>().Add(entity);

            _ = await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            this._dbContext.Set<T>().Remove(entity);

            _ = await this._dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> FindAll<T>() where T : class
        {
            return await this._dbContext.Set<T>().ToListAsync();

        }

        public async Task<T> FindById<T,TId>(TId id) where T : class where TId: class
        {
            return await this._dbContext.Set<T>().FindAsync(id);

        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            this._dbContext.Set<T>().Update(entity);

            _ = await this._dbContext.SaveChangesAsync();
        }
    }
    
    

    public interface IGenericRepository
    {
        Task<List<T>> FindAll<T>() where T : class;
        Task<T> FindById<T,TId>(TId id) where T : class  where TId : class;
        Task CreateAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
    }
    
    
    
    
    public interface IRepository<TEntity,TType> 
        where TEntity : class, new() 
        where TType : class
    {
        IQueryable<TEntity> GetAll();
        Task<List<TEntity>> FindAll();
        Task<TEntity> FindById(TType id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<Boolean> DeleteAsync(TType key);
        
        Task<Boolean> ExistsAsync(TType key);
    }
    
    
    public class Repository<TEntity,TType> : IRepository<TEntity,TType>
        where TEntity : class, new() where TType : class
    {
        private readonly FuncionarioContext _context;
        private readonly IGenericRepository _repository;

        public Repository(FuncionarioContext repositoryPatternDemoContext)
        {
            _context = repositoryPatternDemoContext;
            _repository = new GenericGenericRepository<FuncionarioContext>(repositoryPatternDemoContext);
        }

        public FuncionarioContext Context => _context;


        public async Task CreateAsync(TEntity entity)
        {
           await _repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }
        
        public async Task<Boolean> DeleteAsync(TType key)
        {
            TEntity obj = await FindById(key);
            if (obj == null)
            {
                return false;
            }
            await _repository.DeleteAsync(obj);
            return true;
        }

        public async Task<List<TEntity>> FindAll() 
        {
            return await _repository.FindAll<TEntity>();

        }

        public async Task<TEntity> FindById(TType id)
        {
            return await _repository.FindById<TEntity,TType>(id);

        }

        public async Task UpdateAsync(TEntity entity) 
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<Boolean> ExistsAsync(TType key)
        {
            return await FindById(key) != null;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _context.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possivel recuperar entidades: {ex.Message}");
            }
        }
    }
}
