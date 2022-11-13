using IntegrationAPI.Adapters;
using IntegrationAPI.Controllers.Interfaces;
using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Exceptions;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.BloodBanks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntegrationAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBanksController : ControllerBase
    {
        private readonly IBloodBankService _bloodBankService;

        public BloodBanksController(IBloodBankService bloodBankService)
        {
            _bloodBankService = bloodBankService;
        }

        [HttpPost]
        public ActionResult Create(BloodBankCreationDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BloodBank bank = BloodBankAdapter.FromDTO(entity);
            try
            {
                _bloodBankService.Create(bank);
                return CreatedAtAction("GetById", new { id = bank.Id }, bank);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                BloodBank bloodBank = _bloodBankService.GetById(id);
                if (bloodBank == null)
                {
                    return NotFound();
                }
                _bloodBankService.Delete(bloodBank);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                //throw(new APIKeyExistsException());
                return Ok(_bloodBankService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("test")]
        public ActionResult SendToMQ()
        {
            try
            {
                _bloodBankService.TestSendToMQ();
                return Ok();
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
                BloodBank bloodBank = _bloodBankService.GetById(id);
                if(bloodBank == null)
                {
                    return NotFound();
                }

                return Ok(bloodBank);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("reset/{key}")]
        public ActionResult CheckIfResetKeyExists(string key)
        {
            try
            {
                if (!_bloodBankService.CheckIfPasswordResetKeyExists(key))
                {
                    return BadRequest(new PasswordKeyDoesntExistException());
                }
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("reset/{key}")]
        public ActionResult ActivatePassword(string key, PasswordResetDTO password)
        {
            try
            {
                if (password == null || key == null)
                {
                    return BadRequest();
                }
                BloodBank bloodBank = _bloodBankService.GetBloodBankFromPasswordResetKey(key);

                if (bloodBank == null)
                {
                    return BadRequest(new PasswordKeyDoesntExistException());
                }

                bloodBank.ActivatePassword(password.Password);
                _bloodBankService.Update(bloodBank);
                return Ok();
            } 
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpGet("{id}/{bloodType}/{quantity}")]
        public ActionResult SendBloodRequest(int id, String bloodType, int quantity)
        {
            try
            {
                Boolean hasBlood = _bloodBankService.SendBloodRequest(id, bloodType, quantity);

                return Ok(hasBlood);
            }
            catch(Exception e)
            {
                if(e.ToString().Contains("FailedValidationException"))
                    return BadRequest(e.Message);
                else if(e.ToString().Contains("401"))
                    return Unauthorized("IPA key is invalid!");
                else if (e.ToString().Contains("404"))
                    return NotFound("Bank not found on server side!");
                
                return BadRequest("Can't connect to blood bank server!");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id ,BloodBankDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != entity.Id)
            {
                return BadRequest();
            }
            try
            {
                _bloodBankService.Update(BloodBankAdapter.FromDTO(entity));
            }
            catch
            {
                return BadRequest();
            }
            return Ok(entity);
        }
    }
}
