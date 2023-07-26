using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
namespace HospitalAPI.Controllers.PublicApp
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;

        }

        [HttpPut("medicines/{medicineId}/{quantity}")]
        public ActionResult updateQuantityMedicine(int medicineId, int quantity, Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (medicineId != medicine.Id)
            {
                return BadRequest();
            }

            try
            {
                _medicineService.updateQuantityMedicine(medicineId, quantity, medicine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            return Ok();
        }



    }
}
