using System.Net.Mime;
using Application.DTO.Request;
using Application.DTO.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(500)]
[ProducesResponseType(400)]
[ProducesResponseType(401)]
public class UsersController(IMapper mapper, IUserDomainRepository userDomainRepository) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> SignUp([FromBody] UserRequest userRequest)
    {
        var user = mapper.Map<UserRequest, User>(userRequest);
        await userDomainRepository.SignUp(user);
        var userResponse = mapper.Map<User, UserResponse>(user);
        return StatusCode(201, userResponse);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userDomainRepository.GetAllAsync();
        if (users == null) return NotFound();
        var userResponse = mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users);
        return Ok(userResponse);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserById(long id)
    {
        var user = await userDomainRepository.GetByIdAsync(id);
        if (user == null) return NotFound();
        var userResponse = mapper.Map<User, UserResponse>(user);
        return Ok(userResponse);
    }
    [AllowAnonymous]
    [HttpGet("getUsername/{email}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserByUsername(string email)
    {
        var user = await userDomainRepository.GetUsernameByEmail(email);
        if (user == null) return NotFound();
        var userResponse = mapper.Map<User, UserResponse>(user);
        return Ok(userResponse);
    }
    [AllowAnonymous]
    [HttpGet("getUserRoleByUsername/{username}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserRoleByUsername(string username)
    {
        var currentRole = await userDomainRepository.GetUserRoleByUsername(username);
        if (currentRole == null) return NotFound();
        return Ok(currentRole);
    }
    [AllowAnonymous]
    [HttpGet("sign-in/{email}/{password}")]
    [ProducesResponseType(200)]

    public async Task<IActionResult> SignIn(string email, string password)
    {
        var user = await userDomainRepository.SignIn(email, password);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUserById(long id)
    {
        var user = await userDomainRepository.GetByIdAsync(id);
        if (user == null) return NotFound();
        await userDomainRepository.DeleteAsync(user);
        var userResponse = mapper.Map<User, UserResponse>(user);
        return Ok(userResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUserById(long id, [FromBody] UserRequest userRequest)
    {
        var user = await userDomainRepository.GetByIdAsync(id);
        if (user == null) return NotFound();
        mapper.Map(userRequest, user);
        await userDomainRepository.UpdateAsync(user);
        var userResponse = mapper.Map<User, UserResponse>(user);
        return Ok(userResponse);
    }
}