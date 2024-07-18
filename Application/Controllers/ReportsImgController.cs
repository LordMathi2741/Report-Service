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
[Produces(MediaTypeNames.Application.Json)]
[Route("/api/v1/[controller]")]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
public class ReportsImgController(IReportImgDomainRepository reportImgDomainRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllReportsImg()
    {
        var reportImg = await reportImgDomainRepository.GetAllAsync();
        var reportImgResponse = mapper.Map<IEnumerable<ReportImg>, IEnumerable<ReportImgResponse>>(reportImg);
        return Ok(reportImgResponse);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReportById(long id)
    {
        var reportImg = await reportImgDomainRepository.GetByIdAsync(id);
        if (reportImg == null) return NotFound();
        var reportImgResponse = mapper.Map<ReportImg, ReportResponse>(reportImg);
        return Ok(reportImgResponse);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddReportImg([FromBody] ReportImgRequest reportImgRequest)
    {
        var reportImg = mapper.Map<ReportImgRequest, ReportImg>(reportImgRequest);
        await reportImgDomainRepository.AddReportImgAsync(reportImg);
        var reportImgResponse = mapper.Map<ReportImg, ReportImgResponse>(reportImg);
        return StatusCode(201, reportImgResponse);
    }

    [HttpGet("{filename}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReportImgByFileName(string filename)
    {
        var image = await reportImgDomainRepository.GetReportImgByFileName(filename);
        if (image == null) return NotFound();
        return Ok(image);
    }
}