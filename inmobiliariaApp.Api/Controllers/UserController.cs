using Microsoft.AspNetCore.Mvc;
using inmobiliariaApp.Application;
using inmobiliariaApp.Application.Dtos;
using inmobiliariaApp.Domain.Entities;
using inmobiliariaApp.Domain.Interfaces;

namespace inmobiliariaApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }
    //-------------------------------------------------


    // get user by id 
    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetUserById(id);
        
        if (user ==  null)
            return NotFound(new {message = "User not found"});
        
        return Ok(user);
        
    }

    
    
    // Get all user 
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var user = await _userService.GetAllUsers();
        return Ok(user);
    }

    
    //Create
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            Username = userCreateDto.Username,
            Email = userCreateDto.Email,
            Password = userCreateDto.Password,
            Role = userCreateDto.Role 
        };
        
        var createUser = await _userService.AddUser(user);
        return CreatedAtAction(nameof(GetById), new { id = createUser.Id }, createUser);
    }

    
    // Update
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserCreateDto userCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exist = await _userService.GetUserById(id);
        if (exist == null)
            return NotFound(new { message = $"Error updating user {id}" });
        
        exist.Username =  userCreateDto.Username;
        exist.Email = userCreateDto.Email;
        exist.Password = userCreateDto.Password;
        exist.Role = userCreateDto.Role;

        var updated = await _userService.UpdateUser(exist);
        
        if (!updated )
            return NotFound(new { message = $"Error updating user {id}" });
        return Ok(updated);
    }

    
    // Delete user by ID
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.DeleteUser(id);
        
        if (!deleted)
            return NotFound(new { message = $"Error deleting user {id}" });
        return Ok(deleted);
    }
}