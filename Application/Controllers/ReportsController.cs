
using System.Net.Mime;
using Application.DTO.Request;
using Application.DTO.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;
[Route("/api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
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

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await reportDomainRepository.GetAllAsync();
        var reportsResponse = mapper.Map<IEnumerable<Report> , IEnumerable<ReportResponse>>(reports);
        return Ok(reportsResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReportById(long id)
    {
        var report = await reportDomainRepository.GetByIdAsync(id);
        if (report == null)
        {
            return NotFound();
        }
        var reportResponse = mapper.Map<Report, ReportResponse>(report);
        return Ok(reportResponse);
    }
    
    [HttpGet("{type}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReportByReport(string type)
    {
        var report = await reportDomainRepository.GetReportByTypeAsync(type);
        if (report == null) return NotFound();
        var reportsResponse = mapper.Map<Report , ReportResponse>(report);
        return Ok(reportsResponse);
    }
    
    [HttpGet("brand/{brand}/{year}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTotalReportsByBrandByYear(string brand,int year)
    {
        var reports = await reportDomainRepository.GetTotalReportsByBrandByYearAsync(brand,year);
        if(reports.Count == 0) return NotFound();
        return Ok(reports);
    }

    [HttpGet("get-by-vehicle-identifier/{vehicleIdentifier}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReportByVehicleIdentifier(string vehicleIdentifier)
    {
        var report = await reportDomainRepository.GetReportByVehicleIdentifier(vehicleIdentifier);
        if (report == null) return NotFound();
        return Ok(report);
    }

    [HttpGet("get-by-cylinder-number/{cylinderNumber}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReportByCylinderNumber(string cylinderNumber)
    {
        var report = await reportDomainRepository.GetReportByCylinderNumber(cylinderNumber);
        if (report == null) return NotFound();
        return Ok(report);
    }
    
    
    [HttpGet("getTotalReportsByOperationCenterByYearAndMonth/{year}/{month}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTotalReportsByOperationCenterByYear(int year,int month)
    {
        var reports = await reportDomainRepository.GetTotalReportsByOperationCenterByYearAndMonthAsync(year,month);
        if(reports.Count == 0) return NotFound();
        return Ok(reports);
    }
    
    [HttpGet("countReportsTypeByYearAndMonth/{year}/{month}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CountReportsTypeByYearAndMonth(int year,int month)
    {
        var reports = await reportDomainRepository.CountReportsTypeByYearAndMonthAsync(year,month);
        if(reports.Count == 0) return NotFound();
        return Ok(reports);
    }
    
    [HttpGet("countTotalReportsByYear/{year}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CountTotalReportsByYearAndMonth(int year)
    {
        var reports = await reportDomainRepository.CountTotalReportsByYear(year);
        if(reports.Count == 0) return NotFound();
        return Ok(reports);
    }
    
    
    [HttpGet("img/{certifiedNumber}/{cylinderNumber}/{emitDate}/{vehicleIdentifier}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Task<IActionResult> GetReportImgByType(string certifiedNumber, string cylinderNumber, DateTime emitDate, string vehicleIdentifier)
    {
        var report = reportDomainRepository
                .ReportExistsByImgByCertifiedNumberAndCylinderNumberAndEmitDateAndVehicleIdentifier(certifiedNumber,
                    cylinderNumber, emitDate, vehicleIdentifier);
        if (!report) return Task.FromResult<IActionResult>(NotFound());
        return Task.FromResult<IActionResult>(Ok(report));
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateReport(long id, [FromBody] ReportRequest reportRequest)
    {
        var report = mapper.Map<ReportRequest, Report>(reportRequest);
        await reportDomainRepository.UpdateAsync(id,report);
        var reportResponse = mapper.Map<Report, ReportResponse>(report);
        return Ok(reportResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReport(long id)
    {
        var report = await reportDomainRepository.GetByIdAsync(id);
        if (report == null) return NotFound();
        await reportDomainRepository.DeleteAsync(report);
        var reportResponse = mapper.Map<Report, ReportResponse>(report);
        return Ok(reportResponse);
    }
}