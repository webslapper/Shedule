using System.Linq.Expressions;

namespace Shedule.Data;

public interface IRepository<T> where T : class
{
	Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
	Task<T> GetByIdAsync(int id);
	Task<List<T>> GetAllAsync();
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(int id);
}