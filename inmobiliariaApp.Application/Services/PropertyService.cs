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
    
    
    
    // Delete a property 
    public async Task<bool> DeleteProperty(int id)
    {
        var exits = await _propertyRepository.GetPropertyById(id);
        if (exits == null)
            return false;
        await _propertyRepository.DeleteProperty(id);
        return true;
    }
    
    
    public async Task<Property?> UpdateProperty(int id, PropertyUpdateDto dto, UploadFileDto? image)
    {
        var property = await _propertyRepository.GetPropertyById(id);
        if (property == null)
            return null;

        // Actualizar imagen si viene nueva
        if (image != null)
        {
            // Si ya tiene una imagen previa, la eliminamos
            if (!string.IsNullOrEmpty(property.UrlClaudinary))
            {
                await _cloudinaryService.DeleteImageAsync(property.UrlClaudinary);
            }

            var newUrl = await _cloudinaryService.UploadImageAsync(image);
            property.UrlClaudinary = newUrl;
        }

        // Actualizar solo campos enviados
        if (dto.Title != null) property.Title = dto.Title;
        if (dto.Address != null) property.Address = dto.Address;
        if (dto.Description != null) property.Description = dto.Description;
        if (dto.Price != null) property.Price = dto.Price.Value;
        if (dto.Available != null) property.Available = dto.Available.Value;
        if (dto.Location != null) property.Location = dto.Location;

        await _propertyRepository.UpdateProperty(property);

        return property;
    }

}