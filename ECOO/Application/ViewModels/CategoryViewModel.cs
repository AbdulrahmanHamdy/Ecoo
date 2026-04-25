using System.ComponentModel.DataAnnotations;

namespace ECOO.Application.ViewModels;

/// <summary>
/// ViewModel for creating and editing a category.
/// Contains data annotations for server-side validation.
/// </summary>
public class CategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    [Display(Name = "Category Name")]
    public string Name { get; set; } = string.Empty;
}
