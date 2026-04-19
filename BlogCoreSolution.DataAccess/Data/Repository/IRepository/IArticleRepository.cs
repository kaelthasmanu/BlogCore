using BlogCoreSolution.Models;

namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
}