
using HospitalLibrary.Core.AggregatDoctor;
using HospitalLibrary.Core.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.PrivateApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationStatisticController : ControllerBase
    {
        private readonly ExaminationStatisticService _schedulingStatisticsService;


        public ExaminationStatisticController(ExaminationStatisticService schedulingStatisticsService)
        {
           
            _schedulingStatisticsService = schedulingStatisticsService;
        }

        [HttpGet("GetAllExaminationEventStatistics")]
        public ActionResult GetAllEventStatistics()
        {
            ExaminationStatisticsDTO dto = _schedulingStatisticsService.GetAllEventStatistics();
            return Ok(dto);
        }
    }
}
