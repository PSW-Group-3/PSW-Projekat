using HospitalAPI.Adapters;
using HospitalAPI.DTO;
using HospitalLibrary.Core.DomainService.Interface;
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
    public class GymWorkoutController : ControllerBase
    {
        private readonly IGymWorkoutService _gymWorkoutService;
        private readonly IPatientService _patientService;
        private readonly IWorkoutScoreService _workoutScoreService;

        public GymWorkoutController(IGymWorkoutService workoutService, IPatientService patientService, IWorkoutScoreService workoutScoreService)
        {
            _gymWorkoutService = workoutService;
            _patientService = patientService;
            _workoutScoreService = workoutScoreService;
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

            List<GymWorkout> workouts = (List<GymWorkout>)_gymWorkoutService.GetAllForPatient(patient.Id);
            List<GymWorkoutInfoDTO> dtos = WorkoutAdapter.FromGymWorkoutListToGymWorkoutInfoDTOList(workouts);

            return Ok(dtos);
        }

        //[Authorize]
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
                double score = _workoutScoreService.CalculateGymWorkoutScore(workout.Exercises.Count, workout.Type, workout.GetNumberOfSetsAndReps());
                workout.Score = score;
                _gymWorkoutService.Create(workout);

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
