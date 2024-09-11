using APICatalogo.Context;
using APICatalogo.Repositories.Category;
using APICatalogo.Repositories.Produto;

namespace APICatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepository;

    private ICategoryRepository? _categoryRepository;

    public AppDbContext _context;


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
        }
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Dispose()
    {
        await _context.DisposeAsync();
    }
}
