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
[AllowAnonymous]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(500)]
[ProducesResponseType(400)]
public class UsersController(IMapper mapper, IUserDomainRepository userDomainRepository) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest userRequest)
    {
        var user = mapper.Map<UserRequest, User>(userRequest);
        await userDomainRepository.AddAsync(user);
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

    [HttpGet("getUserRoleByUsername/{username}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserRoleByUsername(string username)
    {
        var currentRole = await userDomainRepository.GetUserRoleByUsername(username);
        if (currentRole == null) return NotFound();
        return Ok(currentRole);
    }

    [HttpGet("{email}/{password}")]
    [ProducesResponseType(200)]

    public async Task<IActionResult> GetUserByEmailAndPassword(string email, string password)
    {
        var user = await userDomainRepository.GetUserByEmailAndPassword(email, password);
        if (user == null) return NotFound();
        var userResponse = mapper.Map<User, UserResponse>(user);
        return Ok(userResponse);
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