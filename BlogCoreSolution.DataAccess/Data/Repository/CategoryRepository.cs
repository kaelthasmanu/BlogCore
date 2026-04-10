using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogCoreSolution.DataAccess.Data.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public void Update(Category category)
    {
        var obj = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
        obj.Name = category.Name;
        obj.Order =  category.Order;
    }
}