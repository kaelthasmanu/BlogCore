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
        // Return an empty model so the strongly-typed view has a non-null Model instance
        return View(new BlogCoreSolution.Models.Category());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            // Ensure CreatedDate is set when creating a new category
            category.CreatedDate = DateTime.UtcNow;
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