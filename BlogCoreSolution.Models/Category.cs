using System.ComponentModel.DataAnnotations;
namespace BlogCoreSolution.Models;

public class Category
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    [Required(ErrorMessage = "Category name is required.")]
    [Display(Name = "Category name")]
    public string Name { get; set; }
    
    [Display(Name = "Order Visualization")]
    [Range(0,10, ErrorMessage = "Order must be between 0 and 10 characters.")]
    public int Order { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
}