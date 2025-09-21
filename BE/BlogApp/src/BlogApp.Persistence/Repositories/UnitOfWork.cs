using BlogApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using BlogApp.Core.Repository;

namespace BlogApp.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlogAppContext _context;
    public UnitOfWork(BlogAppContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
