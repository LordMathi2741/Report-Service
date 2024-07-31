using System.Net.Mime;
using Application.DTO.Request;
using Application.DTO.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(500)]
[ProducesResponseType(400)]
public class ClientsController(IClientDomainRepository clientDomainRepository, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateClient([FromBody] ClientRequest clientRequest)
    {
        var client =  mapper.Map<ClientRequest, Client>(clientRequest);
        await clientDomainRepository.AddAsync(client);
        var clientResponse = mapper.Map<Client, ClientResponse>(client);
        return StatusCode(201, clientResponse);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateClient(long id, [FromBody] ClientRequest clientRequest)
    {
        var client =  mapper.Map<ClientRequest, Client>(clientRequest);
        var updatedClient = await clientDomainRepository.UpdateAsync(id, client);
        if (updatedClient == null) return NotFound();
        var clientResponse = mapper.Map<Client, ClientResponse>(updatedClient);
        return Ok(clientResponse);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteClient(long id)
    {
        var client = await clientDomainRepository.GetByIdAsync(id);
        if (client == null) return NotFound();
        var deletedClient = await clientDomainRepository.DeleteAsync(client);
        var clientResponse = mapper.Map<Client, ClientResponse>(deletedClient);
        return Ok(clientResponse);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetClient(long id)
    {
        var client = await clientDomainRepository.GetByIdAsync(id);
        if (client == null) return NotFound();
        var clientResponse = mapper.Map<Client, ClientResponse>(client);
        return Ok(clientResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetClients()
    {
        var clients = await clientDomainRepository.GetAllAsync();
        var clientResponses = mapper.Map<IEnumerable<Client>, IEnumerable<ClientResponse>>(clients);
        return Ok(clientResponses);
    }
}