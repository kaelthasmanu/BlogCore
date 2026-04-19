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
        var categoryList = _containerWork.Article.GetAll();
        return Json(new { data = categoryList });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var objFromDb = _containerWork.Category.Get(id);
        if (objFromDb == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        _containerWork.Category.Delete(objFromDb);
        _containerWork.Save();
        return Json(new { success = true, message = "Delete successful" });
    }

    #endregion
}