namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository;

public interface IContainerWork : IDisposable
{
    ICategoryRepository Category { get; }
    void Save();
}