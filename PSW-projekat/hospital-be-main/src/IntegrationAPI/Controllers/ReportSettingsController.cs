using IntegrationAPI.Adapters;
using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntegrationAPI.Controllers
{
    [Authorize(Roles = "Manager, Doctor")]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportSettingsController : ControllerBase
    {
        private readonly IReportSettingsService _reportSettingsService;

        public ReportSettingsController(IReportSettingsService reportSettingsService)
        {
            _reportSettingsService = reportSettingsService;
        }

        [HttpPut]
        public ActionResult Update(ReportSettingsDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ReportSettings settings = ReportSettingsAdapter.FromDTO(entity);
            try
            {
                _reportSettingsService.Update(settings);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(entity);
        }

        [HttpPost]
        public ActionResult Create(ReportSettingsDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ReportSettings settings = ReportSettingsAdapter.FromDTO(entity);
            try
            {
                _reportSettingsService.Create(settings);
                return CreatedAtAction("GetById", new {id = settings.Id}, settings);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_reportSettingsService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("setting")]
        public ActionResult GetFirst()
        {
            try
            {
                ReportSettings settings = _reportSettingsService.GetFirst();
                if (settings == null)
                {
                    return NotFound();
                }

                return Ok(settings);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                ReportSettings settings = _reportSettingsService.GetById(id);
                if (settings == null)
                {
                    return NotFound();
                }

                return Ok(settings);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                ReportSettings settings = _reportSettingsService.GetById(id);
                if (settings == null)
                {
                    return NotFound();
                }
                _reportSettingsService.Delete(settings);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
