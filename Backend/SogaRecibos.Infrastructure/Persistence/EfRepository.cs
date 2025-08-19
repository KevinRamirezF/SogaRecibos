using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SogaRecibos.Application.Abstractions.Persistence;

namespace SogaRecibos.Infrastructure.Persistence;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public EfRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        _context.Set<T>().FindAsync(new object[] { id }, ct).AsTask();

    public async Task AddAsync(T entity, CancellationToken ct = default) =>
        await _context.Set<T>().AddAsync(entity, ct);

    public Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken ct = default)
    {
        var query = ApplySpecification(spec);
        return await query.ToListAsync(ct);
    }

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken ct = default)
    {
        var query = ApplySpecification(spec);
        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken ct = default)
    {
        var query = ApplySpecification(spec);
        return await query.AnyAsync(ct);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T>? spec) =>
        spec is null
            ? _context.Set<T>().AsQueryable()
            : new SpecificationEvaluator().GetQuery(_context.Set<T>().AsQueryable(), spec);
}