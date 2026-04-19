using BlogCoreSolution.DataAccess.Data.Repository.IRepository;

namespace BlogCoreSolution.DataAccess.Data.Repository;

public class ContainerWork : IContainerWork
{
    private readonly ApplicationDbContext _dbContext;
    public ContainerWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Category = new CategoryRepository(_dbContext);
        Article = new ArticleRepository(_dbContext);
    }
    public ICategoryRepository Category { get; private set; }
    public IArticleRepository Article { get; private set; }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

}