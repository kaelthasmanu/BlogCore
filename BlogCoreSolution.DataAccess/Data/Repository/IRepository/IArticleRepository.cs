using BlogCoreSolution.Models;

namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository;

public interface IArticleRepository : IRepository<Article>
{
    void Update(Article article);
}