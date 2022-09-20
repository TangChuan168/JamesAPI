using JamesAPI.Domian.Contracts;
using JamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JamesAPI.Domian.Services
{
    public class Repository<T> : IRepository<T> where T :  class, IguidInterface, new()
    {
        private readonly Db _Db;
        private readonly DbSet<T> _DbSet;
        private DbSet<User> _UserDBset;
        public Repository(
            Db dbcontext
            )
        {
            _Db = dbcontext;
            _DbSet = _Db.Set<T>();
            _UserDBset = _Db.Set<User>();       
        }
        public IQueryable<T> Collection => throw new NotImplementedException();

        public async Task Add(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                await _DbSet.AddAsync(entity);
                _Db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public T Create()
        {
            var entity = new T();
            return entity;
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _UserDBset.Where(x => x.Email == email).FirstOrDefaultAsync();
            if(user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var result = await _UserDBset.FirstOrDefaultAsync(x =>x.Uid == id);
            return result;
        }

        public IQueryable<T> GetQueryable(bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        //#updating User
        public async Task UserUpdate(User entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                var res = await _UserDBset.FirstOrDefaultAsync(x=>x.Uid == entity.Uid);
                res.AuthCode= entity.AuthCode;
                _UserDBset.Update(res);
                _Db.SaveChanges(); 
               
            }
            catch (Exception e)
            {
                throw new Exception("Exception:" + e);
            }
        }

        public async Task<bool> FindUserByEmail(string email)
        {
            var user = await _UserDBset.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
