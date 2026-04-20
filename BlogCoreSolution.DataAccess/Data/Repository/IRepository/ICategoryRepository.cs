using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);

    IEnumerable<SelectListItem> CategoriesList();
}