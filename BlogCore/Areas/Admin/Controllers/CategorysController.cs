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
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        Category category = new Category();
        category = _containerWork.Category.Get(id);

        if (category == null)
        {
            return NotFound();
        }
        // Return the model fetched from the database so the view is populated
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        // Load the existing entity and update only editable properties
        var existing = _containerWork.Category.Get(category.Id);
        if (existing == null)
        {
            return NotFound();
        }

        existing.Name = category.Name;
        existing.Order = category.Order;

        _containerWork.Category.Update(existing);
        _containerWork.Save();

        return RedirectToAction(nameof(Index));
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