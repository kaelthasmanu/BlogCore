using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriasController : Controller
{
    private readonly IContainerWork _containerWork;
    
    public CategoriasController(IContainerWork containerWork)
    {
        _containerWork = containerWork;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    #region API Calls
    public IActionResult GetAll()
    {
        var categoryList = _containerWork.Category.GetAll();
        return Json(new { data = categoryList });
    }
    

    #endregion
}