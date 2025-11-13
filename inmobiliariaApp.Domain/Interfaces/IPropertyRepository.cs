using inmobiliariaApp.Domain.Entities;

namespace inmobiliariaApp.Domain.Interfaces;

public interface IPropertyRepository 
{
    Task<Property> GetPropertyById(int id);
    Task<IEnumerable<Property>> GetAllProperty();
    Task<Property> AddProperty(Property property);
    Task<Property> UpdateProperty(Property property);
    Task DeleteProperty(int id);
}