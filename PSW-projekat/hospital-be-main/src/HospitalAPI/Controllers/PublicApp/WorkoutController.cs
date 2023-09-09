﻿using HospitalAPI.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

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
