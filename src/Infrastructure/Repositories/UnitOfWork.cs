using Application.Contracts.Infrastructure;
using Infrastructure.DbContext;

namespace Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly RestaurantDbContext _context;
    public IRestaurantRepository RestaurantRepository => new RestaurantRepository(_context);
    public IDishRepository DishRepository => new DishRepository(_context);
    public IUserRepository UserRepository => new UserRepository(_context);

    public UnitOfWork(RestaurantDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) _context?.Dispose();
    }
}