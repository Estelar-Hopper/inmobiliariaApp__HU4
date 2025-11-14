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

    // get propertys by ID
    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetPropertyById(int id)
    {
       var  property = await _propertyService.GetPropertyById(id);

       if (property == null)
           return NotFound(new { message = $"property with {id} not found "});
       
       return Ok(property);
    }
    
    
    
    // get all propertys 
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllProperty()
    {
        var properties = await _propertyService.GetAllProperty();
        return Ok(properties);
    }
    
    
    
    // Create a new property 
    [HttpPost("create")]
    public async Task<IActionResult> AddProperty([FromBody] PropertyCreateDto propertyDto) //[FromBody] force the Web API to read a simple type from the request body
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        
        // Map The DTO to model
        var property = new Property
        {
            Title = propertyDto.Title,
            Address = propertyDto.Address,
            Description = propertyDto.Description,
            Price = propertyDto.Price,
            Available = propertyDto.Available,
            Location = propertyDto.Location,
            UrlClaudinary = propertyDto.UrlClaudinary
        };
        
        var createdProperty = await _propertyService.AddProperty(property);
        
        return CreatedAtAction(nameof(GetPropertyById), new { id = createdProperty.Id }, createdProperty);
    }
    
    
    
    
    //Update a property By ID
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PropertyCreateDto propertyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exits = await _propertyService.GetPropertyById(id);
        if (exits == null)
            return NotFound(new { message = $"Property with ID {id} not found" });
        
        exits.Title = propertyDto.Title;
        exits.Address = propertyDto.Address;
        exits.Description = propertyDto.Description;
        exits.Price = propertyDto.Price;
        exits.Available = propertyDto.Available;
        exits.Location = propertyDto.Location;
        exits.UrlClaudinary = propertyDto.UrlClaudinary;
        
        var updatedProperty = await _propertyService.UpdateProperty(exits);
        
        if (!updatedProperty)
            return StatusCode(500,new { message = $"Error updating property with ID {id}" });
        
        return NoContent();
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