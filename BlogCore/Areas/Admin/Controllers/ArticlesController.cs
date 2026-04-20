using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers;

[Area("Admin")]
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