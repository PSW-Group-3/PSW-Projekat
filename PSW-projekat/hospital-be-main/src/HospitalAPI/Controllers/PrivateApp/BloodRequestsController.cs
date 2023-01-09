using HospitalLibrary.Core.IntegrationConnection;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.PrivateApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestController : ControllerBase
    {
        private readonly IIntegrationConnection _integrationConnection;


        public BloodRequestController(IIntegrationConnection integrationConnection)
        {
            this._integrationConnection = integrationConnection;
        }

        [HttpGet]
        public ActionResult GetAllRequest()
        {
            try
            {
                return Ok(_integrationConnection.GetBloodRequests());
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("requests/{bloodType}")]
        public ActionResult GetAllRequestByType(BloodType bloodType)
        {
            try
            {
                return Ok(_integrationConnection.GetBloodRequestsByBlood(bloodType));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
