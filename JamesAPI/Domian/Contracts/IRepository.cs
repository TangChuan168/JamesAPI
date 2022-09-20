using JamesAPI.Models;
using System.Linq.Expressions;

namespace JamesAPI.Domian.Contracts
{
    public interface IRepository<T> where T : class, new()
    {
        T Create();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        Task<User> GetUserByIdAsync(Guid id);
        Task<bool> FindUserByEmail(string email);
        Task<User> GetByEmailAsync(string email); 
        Task Add(T entity);
        Task UserUpdate(User entity);
        Task Delete(T entity);
        IQueryable<T> Collection { get; }
        IQueryable<T> GetQueryable(bool includeDeleted = false);
    }
}
