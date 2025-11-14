using inmobiliariaApp.Domain.Entities;

public class Property
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public double Price { get; set; } 
    public bool Available { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? UrlClaudinary { get; set; } = string.Empty;

}
