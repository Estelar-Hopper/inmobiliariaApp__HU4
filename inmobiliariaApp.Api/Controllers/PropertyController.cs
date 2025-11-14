using Microsoft.AspNetCore.Mvc;
using inmobiliariaApp.Application;
using inmobiliariaApp.Application.Dtos;
using inmobiliariaApp.Domain.Entities;
using inmobiliariaApp.Infrastructure.Data;

namespace inmobiliariaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PropertyController : ControllerBase
{
    private readonly PropertyService _propertyService;
    
    public PropertyController(PropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    // ------------------------------------------------------------------

    // get properties by ID
    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetPropertyById(int id)
    {
       var  property = await _propertyService.GetPropertyById(id);

       if (property == null)
           return NotFound(new { message = $"property with {id} not found "});
       
       return Ok(property);
    }
    
    
    
    // get all properties
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllProperty()
    {
        var properties = await _propertyService.GetAllProperty();
        return Ok(properties);
    }
    
    
    
    // Create a new property 
    [HttpPost("create")]
    public async Task<IActionResult> AddProperty([FromForm] PropertyCreateDto propertyDto, IFormFile image) 
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        UploadFileDto? dto = null;
        
        if (image != null)
        {
            dto = new UploadFileDto
            {
                FileName = image.FileName,
                FileStream = image.OpenReadStream()
            };
        }
        
        // Map The DTO to model
        var property = new Property
        {
            Title = propertyDto.Title,
            Address = propertyDto.Address,
            Description = propertyDto.Description,
            Price = propertyDto.Price,
            Available = propertyDto.Available,
            Location = propertyDto.Location,
        };
        
        var createdProperty = await _propertyService.AddProperty(property, dto);
        
        return CreatedAtAction(nameof(GetPropertyById), new { id = createdProperty.Id }, createdProperty);
    }
    
    
    
    //Update a property By ID
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromForm] PropertyUpdateDto dto, IFormFile? image)
    {
        UploadFileDto? fileDto = null;

        if (image != null)
        {
            fileDto = new UploadFileDto
            {
                FileName = image.FileName,
                FileStream = image.OpenReadStream()
            };
        }

        var updated = await _propertyService.UpdateProperty(id, dto, fileDto);

        if (updated == null)
            return NotFound(new { message = $"Property with ID {id} not found" });

        return Ok(updated);
    }
    
    
    
    // Delete property by ID
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteProperty(int id)
    {
        var DeletedProperty = await _propertyService.DeleteProperty(id);
        
        if (!DeletedProperty)
            return NotFound(new { message = $"Property with ID {id} not found" });
        
        return Ok(DeletedProperty);
    }

}