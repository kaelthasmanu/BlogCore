using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BlogCoreSolution.DataAccess.Data.Repository;
using BlogCoreSolution.DataAccess.Data.Repository.IRepository;


public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext Context;
    internal DbSet<T> dbSet;
    public Repository(DbContext context)
    {
        this.Context = context;
        this.dbSet = context.Set<T>();
    }
    public T Get(int id)
    {
        return dbSet.Find(id);
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        
        return query.ToList();
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties = "")
    {
        IQueryable<T> query = dbSet.Where(filter);
        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                         StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return query.FirstOrDefault();
    }

    public void Create(T entity)
    {
        dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public void Delete(int id)
    {
        T entityDeleted = dbSet.Find(id);
        if (entityDeleted != null)
        {
            dbSet.Remove(entityDeleted);
        }
    }
}