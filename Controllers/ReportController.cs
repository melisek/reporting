﻿using Microsoft.AspNetCore.Mvc;
using szakdoga.BusinessLogic;
using szakdoga.Models;
using szakdoga.Models.Dtos;
using szakdoga.Models.Dtos.ReportDtos;

namespace szakdoga.Controllers
{
    [Route("api/reports")]
    public class ReportController : Controller
    {
        private IReportRepository _reportRepository;
        private IReportDashboardRelRepository _reportDashboardRel;

        public ReportController(IReportRepository reportRepository, IReportDashboardRelRepository repDashRel)
        {
            _reportRepository = reportRepository;
            _reportDashboardRel = repDashRel;
        }

        [HttpGet("GetStyle/{reportGUID}")]
        public IActionResult GetRiportStyle(string reportGUID)
        {
            if (string.IsNullOrEmpty(reportGUID)) return BadRequest("Empty GUID!");

            using (var reportManager = new ReportManager(_reportRepository, _reportDashboardRel))
            {
                var report = reportManager.GetReportStyle(reportGUID);
                if (report == null)
                    return NotFound();
                else
                    return Ok(reportManager.GetReportStyle(reportGUID));//OK 200 as státuszkódja van
            }
        }

        [HttpPost("Create")]
        public IActionResult CreateReport([FromBody] CreateReportDto report)
        {
            if (report == null)     //ha nem lehet a bemeneti json-t serializálni a megadott objektumba akk null lesz az értéke
                return BadRequest("Invalid Dto!");
            if (!ModelState.IsValid)//ha nem felelt meg a DataAnnotation atribútumoknak - nem a legjobb adatellenőrzésre a dataannotations, mert keverve vannak az ellenőrzési helye
                                    //hozzá lehet adni itt is hibát, ellenőrzést, ajánlás: FluenValidation:  library- lambdákkal lehet megkötéseket definiálni
                return BadRequest(ModelState); //400-as hibakód

            using (var reportManager = new ReportManager(_reportRepository, _reportDashboardRel))
            {
                var guid = reportManager.CreateReport(report);
                if (!string.IsNullOrEmpty(guid))
                    return Created(string.Empty, guid);
                else
                    return BadRequest("Could not save.");
                //Created()//lehetne még createatute()-akkor megadná h hogy tudja elérni tehát: GetReport/ReportGUID
            }
        }

        [HttpPut("Update/{reportGUID}")]
        public IActionResult UpdateReport([FromBody] UpdateReportDto report, string reportGUID)
        {
            if (report == null) return BadRequest("Invalid Dto");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            using (var reportManager = new ReportManager(_reportRepository, _reportDashboardRel))
            {
                if (reportManager.UpdateReport(report, reportGUID))
                    return NoContent();
                else
                    return BadRequest("Report GUID is not valid.");
            }
        }

        [HttpDelete("Delete/{reportGUID}")]
        public IActionResult DeleteReport(string reportGUID)
        {
            if (string.IsNullOrEmpty(reportGUID))
                return BadRequest("Empty GUID!");

            using (var reportManager = new ReportManager(_reportRepository, _reportDashboardRel))
            {
                if (reportManager.DeleteReport(reportGUID))
                    return NoContent();
                else
                    return BadRequest();
            }
        }
        [HttpPost("GetAll")]
        public IActionResult GetAll([FromBody] GetAllDto filter)
        {
            if (filter == null) return BadRequest("Wrong structure!");

            using (var reportManager = new ReportManager(_reportRepository, _reportDashboardRel))
            {
                AllReportDto report = reportManager.GetAllReport();
                if (report == null)
                    return NotFound();
                else
                    return Ok(report);
            }
        }

    }
}