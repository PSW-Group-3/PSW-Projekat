using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Service.TenderStatistic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntegrationAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TenderStatisticsController : Controller
    {
        private readonly ITenderStatisticsService _tenderStatisticsService;

        public TenderStatisticsController(ITenderStatisticsService tenderStatisticsService)
        {
            _tenderStatisticsService = tenderStatisticsService;
        }

        [HttpPost]
        public ActionResult CreateStatisticForHospital(DatesDTO datesDTO)
        {
            try
            {
            _tenderStatisticsService.CreateStatisticsBloodType(datesDTO.Start, datesDTO.End);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
