using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Aggregate;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.PrivateApp
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statisticsService;
        private readonly SchedulingStatisticsService _schedulingStatisticsService;


        public StatisticsController(StatisticsService statisticsService, SchedulingStatisticsService schedulingStatisticsService)
        {
            _statisticsService = statisticsService;
            _schedulingStatisticsService = schedulingStatisticsService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult GetStatistics()
        {
            //TODO vraca DTO sa statistikom
            return Ok(_statisticsService.GetStatistics());
        }

        [HttpGet("GetAllEventStatistics")]
        public ActionResult GetAllEventStatistics()
        {
            SchedulingStatisticsDTO dto = _schedulingStatisticsService.GetAllEventStatistics();
            return Ok(dto);
        }

    }
}
