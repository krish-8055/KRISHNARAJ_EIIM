using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class ProductModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
}