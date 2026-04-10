using System.Linq.Expressions;

namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository;

public interface IRepository<T> where T : class
{
    T Get(int id);

    IEnumerable<T> GetAll(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = ""
    );
    
    T GetFirstOrDefault(
        Expression<Func<T, bool>> filter,
        string includeProperties = ""
    );
    
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(int id);

}