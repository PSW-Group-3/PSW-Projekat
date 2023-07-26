using HospitalAPI.Adapters;
using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BloodController : ControllerBase
    {
        private readonly IBloodService _bloodService;

        public BloodController(IBloodService bloodService)
        {
            _bloodService = bloodService;

        }

        [HttpGet]
        public ActionResult GetAllBlood()
        {
            try
            {
                return Ok(_bloodService.GetAll());
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPut("bloods/{bloodId}/{quantity}")]
        public ActionResult updateQuantityMedicine(int bloodId, int quantity, Blood blood)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bloodId != blood.Id)
            {
                return BadRequest();
            }

            try
            {
                _bloodService.updateQuantityBlood(bloodId, quantity, blood);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            return Ok();
        }
        
        [HttpPost("takeOrder")]
        public ActionResult TakeOrder(List<BloodOrderDto> entity)
        {
            try
            {
                _bloodService.handleBloodRequest(entity);
                return Ok(entity);
            }
            catch
            {
                return BadRequest("bad");
            }
        }

        [HttpGet("emergency/{bloodType}/{amount}")]
        public ActionResult UpdateQuantity(BloodType bloodType, int amount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _bloodService.updateEmergency(amount, bloodType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            return Ok();
        }

        [HttpPut("Store")]
        public ActionResult StoreBlood(BloodForStoringDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Blood blood = BloodForStoringAdapter.FromDTO(entity);
                return Ok(_bloodService.StoreBlood(blood));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}
