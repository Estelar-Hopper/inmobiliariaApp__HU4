namespace inmobiliariaApp.Application.Dtos;

public class PropertyCreateDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}