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
}