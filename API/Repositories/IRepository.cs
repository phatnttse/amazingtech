namespace API.Repositories
{
    public interface IRepository<T> where T : class    
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(string id);
        Task<T> UpdateAsync(T entity);
        IQueryable<T> GetQueryable();
    }
}
