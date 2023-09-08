using HospitalAPI.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IPersonService _personService;
        private readonly IPatientService _patientService;

        public WorkoutController(IWorkoutService workoutService, IPersonService personService, IPatientService patientService)
        {
            _workoutService = workoutService;
            _personService = personService;
            _patientService = patientService;
        }

        //[Authorize]
        [HttpGet("all/{personId}")]
        public ActionResult GetAllForPatient(int patientId)
        {
            return Ok(_workoutService.GetAllForPatient(patientId));
        }

        //[Authorize]
        [HttpPost("add")]
        public ActionResult AddWorkout(WorkoutDTO dto)
        {
            Patient patient = _patientService.getPatientByPersonId(dto.PersonId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            try
            {
                Workout workout = new(dto.Type, DateTime.Today, TimeSpan.FromMinutes(dto.Duration), dto.Description, patient);
                patient.UpdateHealthScore(workout.Score);
                _patientService.Update(patient);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
