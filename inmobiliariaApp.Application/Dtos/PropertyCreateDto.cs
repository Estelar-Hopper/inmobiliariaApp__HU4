using System.ComponentModel.DataAnnotations;

namespace inmobiliariaApp.Application.Dtos;

public class PropertyCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Address { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo.")]
    public double Price { get; set; }
    
    [Required]
    public bool Available { get; set; }
    
    [Required]
    public string Location { get; set; } = string.Empty;
    
    [Required]
    [Url]
    public string? UrlClaudinary { get; set; } = string.Empty;

}