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

    public async Task<IEnumerable<T>> GetAll() => await _dbTable.ToListAsync();

    public async Task<T?> GetById(object? id) => await _dbTable.FindAsync(id);

    public async Task Insert(T obj) => await _dbTable.AddAsync(obj);

    public Task Update(T obj) => Task.FromResult(_dbTable.Update(obj));

    public Task Delete(object id) => Task.FromResult(_dbTable.Remove(_dbTable.Find(id)));

    public async Task Save() => await _context.SaveChangesAsync();
}