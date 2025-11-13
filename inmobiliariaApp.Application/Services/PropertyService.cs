using inmobiliariaApp.Domain.Entities;
using inmobiliariaApp.Domain.Interfaces;

namespace inmobiliariaApp.Application;

public class PropertyService
{
    private readonly IPropertyRepository  _propertyRepository;

    ///dependency injection
    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }
    
    //-------------------Services for products----------------------//

    // List all products
    public async Task<IEnumerable<Property>> GetAllProperty()
    {
        return await _propertyRepository.GetAllProperty();
    }
    
    //list producst by ID

    public async Task<Property> GetPropertyById(int id)
    {
        return await _propertyRepository.GetPropertyById(id);
    }
    
    // Create a new product
    public async Task<Property> AddProperty(Property property)
    {
        await _propertyRepository.AddProperty(property);
        return property;
    }
    
    //update a product

    public async Task<bool> UpdateProperty(Property property)
    {
        var exits = await _propertyRepository.GetPropertyById(property.Id);
        
        if (exits == null)
         return false;
        
        await _propertyRepository.UpdateProperty(property);
        return true;
    }
    // Delete a product 
    public async Task<bool> DeleteProperty(int id)
    {
        var exits = await _propertyRepository.GetPropertyById(id);
        if (exits == null)
            return false;
        await _propertyRepository.DeleteProperty(id);
        return true;
    }
    
    
}