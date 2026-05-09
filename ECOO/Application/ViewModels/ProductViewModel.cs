using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECOO.Application.ViewModels;


public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100,000.")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "Image URL")]
    [StringLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a category.")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    // Read-only display fields populated from navigation property
    public string? CategoryName { get; set; }

    // Populated by the controller for the create/edit form drop-down
    public SelectList? Categories { get; set; }
}
