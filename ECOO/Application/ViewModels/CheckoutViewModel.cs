using System.ComponentModel.DataAnnotations;

namespace ECOO.Application.ViewModels;

/// <summary>
/// ViewModel for the checkout form.
/// Combines customer/shipping details with a read-only cart summary.
/// </summary>
public class CheckoutViewModel
{
    // ── Customer details ───────────────────────────────────────────

    [Required(ErrorMessage = "Your name is required.")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
    [Display(Name = "Full Name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [Display(Name = "Email Address")]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Shipping address is required.")]
    [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
    [Display(Name = "Shipping Address")]
    public string ShippingAddress { get; set; } = string.Empty;

    // ── Cart summary (read-only, not bound from form) ──────────────

    public CartViewModel Cart { get; set; } = new();
}
