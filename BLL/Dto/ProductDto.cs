using DAL.Models;

namespace BLL.Dto;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public decimal Price { get; set; }
    public int? CategoryId { get; set; }
}
