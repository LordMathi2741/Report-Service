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
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
[Route("/api/v1/[controller]")]
public class ReportsController(IReportDomainRepository reportDomainRepository, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateReport([FromBody] ReportRequest reportRequest)
    {
        var report = mapper.Map<ReportRequest, Report>(reportRequest);
        await reportDomainRepository.AddAsync(report);
        var reportResponse = mapper.Map<Report, ReportResponse>(report);
        return StatusCode(201, reportResponse);
    }
}