using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;

namespace BlogCore.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Articles/[action]/{id?}")]
[Route("Admin/Articulos/[action]/{id?}")]
public class ArticlesController : Controller
{
    private readonly IContainerWork _containerWork;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ArticlesController> _logger;

    public ArticlesController(IContainerWork containerWork, IWebHostEnvironment env, ILogger<ArticlesController> logger)
    {
        _containerWork = containerWork;
        _env = env;
        _logger = logger;
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
            // Log ModelState errors to help debugging
            foreach (var kv in ModelState)
            {
                var key = kv.Key;
                var errors = kv.Value.Errors;
                foreach (var err in errors)
                {
                    _logger.LogWarning("ModelState error for {Key}: {Error}", key, err.ErrorMessage ?? err.Exception?.Message);
                }
            }

            // repopulate categories for the dropdown when returning the view
            vm.Categories = _containerWork.Category.CategoriesList();
            return View(vm);
        }

        // handle image upload if present
        if (vm.ImageFile != null && vm.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "articles");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                vm.ImageFile.CopyTo(fileStream);
            }
            // set the UrlImage to the relative path used by the app
            vm.article.UrlImage = Path.Combine("/images/articles", uniqueFileName).Replace("\\", "/");
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