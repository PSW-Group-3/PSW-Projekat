using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace HospitalAPI.Controllers.PublicApp
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TherapyController : ControllerBase
    {
        private readonly ITherapyService _therapyService;
        private readonly IMedicineService _medicineService;
        private readonly IBloodService _bloodService;

        public TherapyController(ITherapyService therapyService, IMedicineService medicineService, IBloodService bloodService)
        {
            _therapyService = therapyService;
            _medicineService = medicineService;
            _bloodService = bloodService;

        }

        [HttpPost]
        public ActionResult Create(Therapy therapy)
        {

            if (!(therapy.Medicine == null))
            {

                therapy.Medicine = _medicineService.GetById(therapy.Medicine.Id);
            }

            if (!(therapy.Blood == null))
            {

                therapy.Blood = _bloodService.GetById(therapy.Blood.Id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _therapyService.Create(therapy);

            return Ok();

        }
    }
}
