using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogCoreSolution.DataAccess.Data.Repository;

public class ArticleRepository : Repository<Article>, IArticleRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ArticleRepository(ApplicationDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public void Update(Article article)
    {
        var obj = _dbContext.Articles.FirstOrDefault(x => x.Id == article.Id);
        if (obj == null)
            return;

        obj.Name = article.Name;
        obj.Description = article.Description;
        obj.UrlImage = article.UrlImage;
        obj.Category = article.Category;
        _dbContext.Articles.Update(obj);
    }
}