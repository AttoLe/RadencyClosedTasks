using Task2.Database.Entities;

namespace Task2.Database.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    public Task<IEnumerable<T>> GetAll();
    public Task<bool> Contains(int id);
    public Task<T?> GetById(int id);
    public Task Insert(T obj);
    public Task Update(T obj);
    public Task Delete(T obj);
    Task Save();
}