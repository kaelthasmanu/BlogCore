using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCoreSolution.Models.ViewModels;

public class ArticleVM
{
    public Article article {get;set;}
    public IEnumerable<SelectListItem> Categories {get;set;}
}