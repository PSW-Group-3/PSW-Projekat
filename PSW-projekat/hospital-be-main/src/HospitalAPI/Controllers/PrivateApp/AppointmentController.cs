using HospitalLibrary.Core.DTOs;
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

    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPersonService _userService;

        public AppointmentController(IAppointmentService appointmentService, IPersonService userService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_appointmentService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            appointment.Doctor = _userService.GetById(appointment.Doctor.Id);
            appointment.Patient = _userService.GetById(appointment.Patient.Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _appointmentService.Create(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.Id)
            {
                return BadRequest();
            }

            try
            {
                _appointmentService.Update(appointment);
            }
            catch(Exception ex)
            {
                //return BadRequest();
                return BadRequest(ex.Message);

            }

            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _appointmentService.Delete(appointment);
            return NoContent();
        }

        [HttpGet("doctor/{doctorId}")]
        public ActionResult GetAllByDoctor(int doctorId)
        {
            return Ok(_appointmentService.GetAllByDoctor(doctorId));
        }  
    }
}
