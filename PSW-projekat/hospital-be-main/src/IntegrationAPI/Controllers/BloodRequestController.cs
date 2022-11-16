using IntegrationAPI.Adapters;
using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.BloodRequests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestController : ControllerBase
    {


        private readonly IBloodRequestService _bloodRequestService;

        public BloodRequestController(IBloodRequestService bloodRequestService)
        {
            _bloodRequestService = bloodRequestService;
        }

        [HttpPost]
        public ActionResult Create(BloodRequestDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BloodRequest bloodRequest = BloodRequestAdapter.FromDTO(entity);
            try
            {
                _bloodRequestService.Create(bloodRequest);
                return CreatedAtAction("GetById", new { id = bloodRequest.Id }, bloodRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                BloodRequest bloodRequest = _bloodRequestService.GetById(id);
                if (bloodRequest == null)
                {
                    return NotFound();
                }

                return Ok(bloodRequest);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
