using HospitalAPI.Adapters;
using HospitalAPI.DTO;
using HospitalLibrary.Core.DomainService.Interface;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class GymWorkoutController : ControllerBase
    {
        private readonly IGymWorkoutService _gymWorkoutService;
        private readonly IWorkoutScoreService _workoutScoreService;

        private readonly IPatientService _patientService;
        private readonly IPatientScoreService _patientScoreService;

        public GymWorkoutController(IGymWorkoutService workoutService, IPatientService patientService, IWorkoutScoreService workoutScoreService, IPatientScoreService patientScoreService)
        {
            _gymWorkoutService = workoutService;
            _patientService = patientService;
            _workoutScoreService = workoutScoreService;
            _patientScoreService = patientScoreService;
        }

        [Authorize(Roles = "Patient")]
        [HttpPut("all/{personId}")]
        public ActionResult GetAllForPatient(int personId, DateRangeDTO dto)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            if (!dto.DateFrom.HasValue || !dto.DateUntil.HasValue)
            {
                return BadRequest("From and until dates are required!");
            }

            List<GymWorkout> workouts = _gymWorkoutService.GetAllForPatientInsideDateRange(patient.Id, dto.DateFrom.Value, dto.DateUntil.Value) as List<GymWorkout>;

            return Ok(WorkoutAdapter.FromGymWorkoutListToGymWorkoutInfoDTOList(workouts));
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("add")]
        public ActionResult AddGymWorkout(AddGymWorkoutDTO dto)
        {
            Patient patient = _patientService.getPatientByPersonId(dto.PersonId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            try
            {
                GymWorkout workout = WorkoutAdapter.FromAddGymWorkoutDTOtoGymWorkout(dto, patient);
                workout.Score = _workoutScoreService.CalculateGymWorkoutScore(workout.Exercises.Count, workout.WorkoutType, workout.GetNumberOfSetsAndReps());
                _gymWorkoutService.Create(workout);

                _patientService.Update(_patientScoreService.UpdatePatientHealtScore(patient, workout.Score));

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
