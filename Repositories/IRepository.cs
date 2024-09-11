using System.Linq.Expressions;

namespace APICatalogo.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> Listar();
    Task<T> BuscarPorId(Expression<Func<T, bool>> predicate);
    Task<T> Criar(T entity);
    Task<T> Editar(T entity);
    Task<T> Deletar(T entity);
}
