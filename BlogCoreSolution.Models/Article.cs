using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BlogCoreSolution.Models;

public class Article
{
    public Article()
    {
            CreatedOn = DateTime.UtcNow;
    }
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Article title is required.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Article content is required.")]
    [Display(Name = "Article content")]
    public string Description { get; set; }
    
    [Display(Name = "Date Created")]
    public DateTime CreatedOn { get; set; }
    
    [DataType(DataType.ImageUrl)]
    [Display(Name = "Image URL")]
    public string UrlImage { get; set; }
    
    [Required(ErrorMessage = "Category is required.")]
    public int? IdCategory { get; set; }
    
    [ForeignKey("IdCategory")]
    [ValidateNever]
    public Category? Category { get; set; }
}