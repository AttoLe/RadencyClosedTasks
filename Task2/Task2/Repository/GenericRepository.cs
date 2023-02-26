using Microsoft.EntityFrameworkCore;

namespace Task2.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private LibraryContext _context;
    private DbSet<T> _dbTable;

    public GenericRepository(LibraryContext context)
    {
        _context = context;
        _dbTable = _context.Set<T>();
    }

    public GenericRepository()
    {
        _context = new LibraryContext();
        _dbTable = _context.Set<T>();
    }

    public Task<IEnumerable<T>> GetAll() => Task.FromResult<IEnumerable<T>>(_dbTable);

    public async Task<T?> GetById(object? id) => await _dbTable.FindAsync(id);

    public async Task Insert(T obj)
    {
        await _dbTable.AddAsync(obj);
        await Save();
    }

    public async Task Update(T obj)
    {
        _dbTable.Update(obj);
        await Save();
    }

    public async Task Delete(T obj)
    {
        _dbTable.Remove(obj);
        await Save();
    }

    public async Task Save() => await _context.SaveChangesAsync();
}