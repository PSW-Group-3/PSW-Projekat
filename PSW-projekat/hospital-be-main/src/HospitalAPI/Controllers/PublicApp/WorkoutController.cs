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
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IPatientService _patientService;
        private readonly IWorkoutScoreService _workoutScoreService;

        public WorkoutController(IWorkoutService workoutService, IPatientService patientService, IWorkoutScoreService workoutScoreService)
        {
            _workoutService = workoutService;
            _patientService = patientService;
            _workoutScoreService = workoutScoreService;
        }

        //[Authorize]
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

            List<Workout> workouts = (List<Workout>)_workoutService.GetAllForPatientInsideDateRange(patient.Id, dto.DateFrom.Value, dto.DateUntil.Value);
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
                double score = _workoutScoreService.CalculateWorkoutScore(dto.Duration, dto.Type);
                Workout workout = WorkoutAdapter.FromAddWorkoutDTOtoWorkout(dto, score, patient);
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
