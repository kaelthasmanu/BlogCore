using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        if (obj == null)
            return;

        obj.Name = category.Name;
        obj.Order = category.Order;
        _dbContext.Categories.Update(obj);
    }

    public IEnumerable<SelectListItem> CategoriesList()
    {
        return _dbContext.Categories.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }
}