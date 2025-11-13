using inmobiliariaApp.Domain.Entities;

public class Property
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

   
    public int? UsersId { get; set; }

    public User? Users { get; set; }
}
