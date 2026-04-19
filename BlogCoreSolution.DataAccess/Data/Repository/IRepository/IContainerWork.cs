namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository;

public interface IContainerWork : IDisposable
{
    ICategoryRepository Category { get; }
    IArticleRepository Article { get; }
    void Save();
}