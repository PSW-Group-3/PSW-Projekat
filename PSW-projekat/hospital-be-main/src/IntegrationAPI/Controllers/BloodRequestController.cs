using IntegrationAPI.Adapters;
using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.BloodRequests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IntegrationAPI.Controllers
{
    //[Authorize(Roles = "Manager, Doctor")]
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
                return Ok(entity);
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

                return Ok(BloodRequestAdapter.ToDTO(bloodRequest));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("accept")]
        public ActionResult AcceptRequest(BloodRequestDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BloodRequest bloodRequest = BloodRequestAdapter.FromDTO(entity);
            try
            {
                _bloodRequestService.AcceptRequest(bloodRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("decline/{id}")]
        public ActionResult DeclineRequest(int id)
        {
            try
            {
                _bloodRequestService.DeclineRequest(id);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("return/{id}")]
        public ActionResult SendBackRequest(int id, [FromBody] string reason)
        {
            try
            {
                _bloodRequestService.SendBackRequest(id, reason);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                List<BloodRequestDTO> blood = new List<BloodRequestDTO>();
                foreach (BloodRequest b in _bloodRequestService.GetAll())
                {
                    blood.Add(BloodRequestAdapter.ToDTO(b));
                }

                return Ok(blood);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("requests/{bloodType}")]
        public ActionResult GetAllByType(String bloodType)
        {
            try
            {
                List<BloodRequestDTO> blood = new List<BloodRequestDTO>();
                foreach (BloodRequest b in _bloodRequestService.GetAllByType(getBloodType(bloodType)))
                {
                    blood.Add(BloodRequestAdapter.ToDTO(b));
                }
                return Ok(blood);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private BloodType getBloodType(String type)
        {
            switch (type)
            {
                case "BMinus":
                    return BloodType.BN;
                case "AMinus":
                    return BloodType.AN;
                case "ABMinus":
                    return BloodType.ABN;
                case "OMinus":
                    return BloodType.ON;
                case "BPlus":
                    return BloodType.BP;
                case "APlus":
                    return BloodType.AP;
                case "ABPlus":
                    return BloodType.ABP;
                case "OPlus":
                    return BloodType.OP;
            }
            throw new Exception("Blood type isn't valid.");
        }

        [HttpPut]
        public ActionResult Update(BloodRequestDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _bloodRequestService.Update(BloodRequestAdapter.FromDTO(entity));
            }
            catch
            {
                return BadRequest();
            }
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                BloodRequest bloodRequest = _bloodRequestService.GetById(id);
                if (bloodRequest == null)
                {
                    return NotFound();
                }
                _bloodRequestService.Delete(bloodRequest);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("doctor/{id}")]
        public ActionResult GetReturnedRequestsForDoctor(int id)
        {
            try
            {
                List<BloodRequestDTO> blood = new List<BloodRequestDTO>();
                foreach (BloodRequest b in _bloodRequestService.GetReturnedRequestsForDoctor(id))
                {
                    blood.Add(BloodRequestAdapter.ToDTO(b));
                }
                return Ok(blood);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("update-from-doctor")]
        public ActionResult UpdateFromDoctor(BloodRequestDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _bloodRequestService.UpdateFromDoctor(BloodRequestAdapter.FromDTO(entity));
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
