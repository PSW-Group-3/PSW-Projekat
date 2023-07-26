using System;
using System.Collections.Generic;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.PublicApp
{
    //[Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentService _treatmentService;
        private readonly IPatientService _patientService;
        private readonly IRoomService _roomService;
        private readonly IBedService _bedService;
        private readonly IBloodService _bloodService;
        private readonly IMedicineService _medicineService;
        private readonly ITherapyService _therapyService;


        public TreatmentController(IPatientService patientService, ITreatmentService treatmentService, IRoomService roomService,
            IBedService bedService, IBloodService bloodService, IMedicineService medicineService, ITherapyService therapyService)
        {
            _patientService = patientService;
            _treatmentService = treatmentService;
            _roomService = roomService;
            _bedService = bedService;
            _bloodService = bloodService;
            _medicineService = medicineService;
            _therapyService = therapyService;

        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<TreatmentDto> treatmentDto = new List<TreatmentDto>();
            foreach (var treatment in _treatmentService.GetAll())
            {
                PatientDto patientDto = new PatientDto(treatment.Patient.Id, treatment.Patient.Person.Name,
                    treatment.Patient.Person.Surname, treatment.Patient.Person.Email.ToString(), treatment.Patient.Person.Role);

                //int id, string number, int floor, RoomType roomType, ICollection<BedDto> bedDtos

                ICollection<BedDto> bedDtos = new List<BedDto>();

                foreach (var bed in treatment.Room.Beds)
                {
                    PatientDto patientDto2 = null;

                    if (bed.Patient != null)
                    {
                        patientDto2 = new PatientDto(bed.Patient.Id, bed.Patient.Person.Name,
                        bed.Patient.Person.Surname, bed.Patient.Person.Email.ToString(), bed.Patient.Person.Role);

                        bedDtos.Add(new BedDto(bed.Id, bed.Name, bed.BedState, patientDto2, bed.Quantity));
                    }
                }

                RoomDto roomDto = new RoomDto(treatment.Room.Id, treatment.Room.Number, treatment.Room.Floor, treatment.Room.RoomType,
                    bedDtos);

                treatmentDto.Add(new TreatmentDto(treatment.Id, patientDto, treatment.ReasonForDischarge, treatment.ReasonForAdmission, treatment.DateAdmission,
                    treatment.DateDischarge, roomDto, treatment.Therapy));

            }

            return Ok(treatmentDto);
        }

        [HttpPost]
        public ActionResult Create(TreatmentDto treatmentDto)
        {
            
            if (!(treatmentDto.Therapy.Medicine == null)) {

                treatmentDto.Therapy.Medicine = _medicineService.GetById(treatmentDto.Therapy.Medicine.Id);
            }

            if (!(treatmentDto.Therapy.Blood == null))
            {
                treatmentDto.Therapy.Blood = _bloodService.GetById(treatmentDto.Therapy.Blood.Id);
            }

            //treatmentDto.Therapy = _therapyService.GetById(treatmentDto.Therapy.Id);

            Treatment treatment = new Treatment();
            treatment.Id = treatmentDto.Id;
            treatment.Patient = _patientService.GetById(treatmentDto.Patient.Id);
            treatment.Room = _roomService.GetById(treatmentDto.RoomDto.Id);
            treatment.DateAdmission = treatmentDto.DateAdmission;
            treatment.ReasonForAdmission = treatmentDto.ReasonForAdmission;
            treatment.ReasonForDischarge = treatmentDto.ReasonForDischarge;
            treatment.Therapy = treatmentDto.Therapy;
            treatment.DateDischarge = DateTime.Now;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _treatmentService.Create(treatment);
            //return CreatedAtAction("GetById", new { id = treatment.Id }, treatment);

            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var treatment = _treatmentService.GetById(id);
            if (treatment == null)
            {
                return NotFound();
            }

            _treatmentService.Delete(treatment);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var treatment = _treatmentService.GetById(id);

            PatientDto patientDto = new PatientDto(treatment.Patient.Id, treatment.Patient.Person.Name,
                    treatment.Patient.Person.Surname, treatment.Patient.Person.Email.ToString(), treatment.Patient.Person.Role);

            ICollection<BedDto> bedDtos = new List<BedDto>();

            foreach (var bed in treatment.Room.Beds)
            {
                PatientDto patientDto2 = null;

                if (bed.Patient != null)
                {
                    patientDto2 = new PatientDto(bed.Patient.Id, bed.Patient.Person.Name,
                    bed.Patient.Person.Surname, bed.Patient.Person.Email.ToString(), bed.Patient.Person.Role);

                    bedDtos.Add(new BedDto(bed.Id, bed.Name, bed.BedState, patientDto2, bed.Quantity));
                }

            }

            RoomDto roomDto = new RoomDto(treatment.Room.Id, treatment.Room.Number, treatment.Room.Floor, treatment.Room.RoomType,
                    bedDtos);

            TreatmentDto treatmentDto = new TreatmentDto(treatment.Id, patientDto, treatment.ReasonForDischarge, treatment.ReasonForAdmission,
                treatment.DateAdmission, treatment.DateDischarge, roomDto, treatment.Therapy);

            if (treatment == null)
            {
                return NotFound();
            }

            return Ok(treatmentDto);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, TreatmentDto treatmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatmentDto.Id)
            {
                return BadRequest();
            }

            Treatment treatment = _treatmentService.GetById(treatmentDto.Id);
            treatment.DateDischarge = treatmentDto.DateDischarge;
            treatment.ReasonForDischarge = treatmentDto.ReasonForDischarge;
            treatment.TreatmentState = HospitalLibrary.Core.Model.Enums.TreatmentState.close;


            try
            {
                Bed bed = _bedService.GetByPatientId(treatment.Patient.Id);
                bed.BedState = HospitalLibrary.Core.Model.Enums.BedState.free;
                bed.Patient = null;

                _bedService.Update(bed);
                _treatmentService.Update(treatment);
            }
            catch (Exception ex)
            {
                //return BadRequest();
                return BadRequest(ex.Message);

            }

            byte[] file = _treatmentService.GeneratePdf(treatment);
            Guid uniqueSuffix = Guid.NewGuid();
            System.IO.File.WriteAllBytes("report" + ".pdf", file);
            //return Ok();
            //return Ok(treatment);
            return File(file, "application/pdf", "report"  + ".pdf");
        }


    }
}
