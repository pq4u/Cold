using System.ComponentModel.DataAnnotations;

namespace Cold.Catalog.Shared.Dto;

public class CategoryDto
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Image { get; set; }
}