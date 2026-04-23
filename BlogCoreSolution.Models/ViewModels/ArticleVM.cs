using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace BlogCoreSolution.Models.ViewModels;

public class ArticleVM
{
    // Initialize to avoid null reference in views
    public Article article { get; set; } = new Article();
    // Categories list for the select; default to empty list
    [Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidateNever]
    public IEnumerable<SelectListItem>? Categories { get; set; } = Enumerable.Empty<SelectListItem>();

    // File uploaded from the Create/Edit form (optional)
    public IFormFile? ImageFile { get; set; }
}