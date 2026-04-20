using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Articles/[action]/{id?}")]
[Route("Admin/Articulos/[action]/{id?}")]
public class ArticlesController : Controller
{
    private readonly IContainerWork _containerWork;

    public ArticlesController(IContainerWork containerWork)
    {
        _containerWork = containerWork;
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        ArticleVM articleVm = new ArticleVM()
        {
            article = new BlogCoreSolution.Models.Article(),
            // ArticleVM defines the property 'Categories' for the select list
            Categories = _containerWork.Category.CategoriesList()
        };
        return View(articleVm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ArticleVM vm)
    {
        if (!ModelState.IsValid)
        {
            // repopulate categories for the dropdown when returning the view
            vm.Categories = _containerWork.Category.CategoriesList();
            return View(vm);
        }

        // ensure creation date is set
        vm.article.CreatedOn = DateTime.UtcNow;
        _containerWork.Article.Create(vm.article);
        _containerWork.Save();

        return RedirectToAction(nameof(Index));
    }
    
    #region API Calls
    public IActionResult GetAll()
    {
        // Include the Category navigation property so DataTables can access category.name
        var articleList = _containerWork.Article.GetAll(includeProperties: "Category");
        return Json(new { data = articleList });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        // Use the Article repository to delete articles (was incorrectly using Category)
        var objFromDb = _containerWork.Article.Get(id);
        if (objFromDb == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        _containerWork.Article.Delete(objFromDb);
        _containerWork.Save();
        return Json(new { success = true, message = "Delete successful" });
    }

    #endregion
}