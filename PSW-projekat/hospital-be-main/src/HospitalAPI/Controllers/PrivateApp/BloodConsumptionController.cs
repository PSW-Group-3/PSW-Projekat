using HospitalAPI.Adapters;
using HospitalAPI.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HospitalAPI.Controllers.PrivateApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BloodConsumptionController : ControllerBase
    {
        private readonly IBloodConsumptionService _bloodConsumptionService;
        private readonly IDoctorService _doctorService;
        private readonly IRoomService _roomService;

        public BloodConsumptionController(IBloodConsumptionService bloodConsumptionService, IDoctorService doctorService, IRoomService roomService)
        {
            this._bloodConsumptionService = bloodConsumptionService;
            this._doctorService = doctorService;
            this._roomService = roomService;
        }

        [HttpPost]
        public ActionResult Create(BloodConsumptionDTO bloodConsumptionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DoctorBloodConsumption bloodConsumption = BloodConsumptionAdapter.FromDTO(bloodConsumptionDTO);
            try
            {
                bloodConsumption.Doctor = _doctorService.GetById(bloodConsumptionDTO.DoctorId);
                if (!_roomService.ReduceBloodCount(bloodConsumption.Blood)) return BadRequest();
                _bloodConsumptionService.Create(bloodConsumption);
                return Ok(bloodConsumptionDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
