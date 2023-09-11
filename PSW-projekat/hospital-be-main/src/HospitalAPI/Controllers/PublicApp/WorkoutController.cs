using HospitalAPI.Adapters;
using HospitalAPI.DTO;
using HospitalLibrary.Core.Model;
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
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IPatientService _patientService;

        public WorkoutController(IWorkoutService workoutService, IPatientService patientService)
        {
            _workoutService = workoutService;
            _patientService = patientService;
        }

        //[Authorize]
        [HttpGet("all/{personId}")]
        public ActionResult GetAllForPatient(int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            List<Workout> workouts = (List<Workout>)_workoutService.GetAllForPatient(patient.Id);
            List<WorkoutInfoDTO> dtos = WorkoutAdapter.FromWorkoutListToWorkoutInfoDTOList(workouts);

            return Ok(dtos);
        }

        //[Authorize]
        [HttpPost("add")]
        public ActionResult AddWorkout(AddWorkoutDTO dto)
        {
            Patient patient = _patientService.getPatientByPersonId(dto.PersonId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            try
            {
                Workout workout = WorkoutAdapter.FromAddWorkoutDTOtoWorkout(dto, patient);
                _workoutService.Create(workout);
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
