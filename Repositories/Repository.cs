using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<T>> Listar()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> BuscarPorId(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> Criar(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public async Task<T> Editar(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public async Task<T> Deletar(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }

}
