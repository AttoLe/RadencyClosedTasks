namespace Task2.Database.Repository;

public interface IGenericRepository<T> where T : class
{
    public IEnumerable<T> GetAll();
    public Task<T?> GetById(object? id);
    public Task Insert(T obj);
    public Task Update(T obj);
    public Task Delete(T obj);
    Task Save();
}