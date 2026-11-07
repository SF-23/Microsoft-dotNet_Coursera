using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static List<User> users = new();

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound($"User with ID {id} not found.");
        }

        return Ok(user);
    }
    
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("Test exception");
    }

    [HttpPost]
    public IActionResult AddUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (users.Any(u => u.Id == user.Id))
        {
            return BadRequest("A user with this ID already exists.");
        }

        users.Add(user);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        try
        {
            if (updatedUser == null)
            {
                return BadRequest("User data is required.");
            }

            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return NotFound();

        users.Remove(user);

        return NoContent();
    }
    
    
    
}