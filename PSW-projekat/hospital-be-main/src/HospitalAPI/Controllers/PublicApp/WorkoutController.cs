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
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IWorkoutScoreService _workoutScoreService;
        private readonly IWorkoutStatisticsService _workoutStatisticsService;

        private readonly IPatientService _patientService;
        private readonly IPatientScoreService _patientScoreService;


        public WorkoutController(IWorkoutService workoutService, IPatientService patientService, IWorkoutScoreService workoutScoreService, IWorkoutStatisticsService workoutStatisticsService, IPatientScoreService patientScoreService)
        {
            _workoutService = workoutService;
            _patientService = patientService;
            _workoutScoreService = workoutScoreService;
            _workoutStatisticsService = workoutStatisticsService;
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

            List<Workout> workouts = _workoutService.GetAllForPatientInsideDateRange(patient.Id, dto.DateFrom.Value, dto.DateUntil.Value) as List<Workout>;

            return Ok(WorkoutAdapter.FromWorkoutListToWorkoutInfoDTOList(workouts));
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("statistics/{personId}")]
        public ActionResult GetWorkoutStatistics(int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }
            
            return Ok(_workoutStatisticsService.GetAllWorkoutsStatistics(patient.Id));
        }

        [Authorize(Roles = "Patient")]
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
                double score = _workoutScoreService.CalculateWorkoutScore(dto.Duration, dto.Type);
                Workout workout = WorkoutAdapter.FromAddWorkoutDTOtoWorkout(dto, score, patient);
                _workoutService.Create(workout);

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
