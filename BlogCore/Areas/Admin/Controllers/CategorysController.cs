using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _containerWork.Category.Create(category);
            _containerWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // If we got here something failed; return to the view with validation messages
        return View(category);
    }

    #region API Calls
    public IActionResult GetAll()
    {
        var categoryList = _containerWork.Category.GetAll();
        return Json(new { data = categoryList });
    }
    

    #endregion
}