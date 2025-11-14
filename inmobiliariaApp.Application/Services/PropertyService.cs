using inmobiliariaApp.Application.Dtos;
using inmobiliariaApp.Domain.Entities;
using inmobiliariaApp.Domain.Interfaces;
using ProductCatalog.Application.Interfaces;

namespace inmobiliariaApp.Application;

public class PropertyService
{
    private readonly IPropertyRepository  _propertyRepository;
    private readonly ICloudinaryService  _cloudinaryService;

    ///dependency injection
    public PropertyService(IPropertyRepository propertyRepository, ICloudinaryService  cloudinaryService)
    {
        _propertyRepository = propertyRepository;
        _cloudinaryService = cloudinaryService;
    }
    
    //-------------------Services for property----------------------//

    // List all properties
    public async Task<IEnumerable<Property>> GetAllProperty()
    {
        return await _propertyRepository.GetAllProperty();
    }
    
    //list property by ID
    public async Task<Property> GetPropertyById(int id)
    {
        return await _propertyRepository.GetPropertyById(id);
    }
    
    
    // Create a new property
    public async Task<Property> AddProperty(Property property, UploadFileDto? image)
    {
        if (image != null)
        {
            var imageurl = await _cloudinaryService.UploadImageAsync(image);
            property.UrlClaudinary = imageurl;
        }
        
        await _propertyRepository.AddProperty(property);
        return property;
    }
    
    
    //update a property

    public async Task<bool> UpdateProperty(Property property)
    {
        var exits = await _propertyRepository.GetPropertyById(property.Id);
        
        if (exits == null)
         return false;
        
        await _propertyRepository.UpdateProperty(property);
        return true;
    }
    
    
    // Delete a property 
    public async Task<bool> DeleteProperty(int id)
    {
        var exits = await _propertyRepository.GetPropertyById(id);
        if (exits == null)
            return false;
        await _propertyRepository.DeleteProperty(id);
        return true;
    }
    
    
}