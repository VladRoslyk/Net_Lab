using System.ComponentModel.DataAnnotations;

namespace Products.Infrastructure.Models;

public class TelevisionModel
{
    [Key]
    public Guid Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required, StringLength(100)]
    public string Description { get; set; }

    [Required]
    public bool HasQualityDisplay { get; set; }

    public ICollection<OrderModel> Orders { get; set; }
}
