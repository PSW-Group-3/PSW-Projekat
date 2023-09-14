using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HospitalAPI.Adapters;
using Microsoft.AspNetCore.Authorization;
using HospitalLibrary.Core.DomainService.Interface;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly MealStatisticsService _mealStatisticsService;
        private readonly IMealQuestionService _mealQuestionService;
        private readonly IPatientService _patientService;
        private readonly IMealScoreService _mealScoreService;


        public MealController(IMealService mealService, MealStatisticsService mealStatisticsService, IMealQuestionService mealQuestionService, IPatientService patientService, IMealScoreService mealScoreService)
        {
            _mealService = mealService;
            _mealStatisticsService = mealStatisticsService;
            _mealQuestionService = mealQuestionService;
            _patientService = patientService;
            _mealScoreService = mealScoreService;
        }

        [Authorize]
        [HttpGet("mealquestions/{mealType}")]
        public ActionResult GetAllMealQuestionsByType(MealType mealType)
        {
            return Ok(MealQuestionAdapter.ToListDTO((List<MealQuestion>)_mealQuestionService.GetAllMealQuestionsByMealType(mealType)));
        }

        [Authorize]
        [HttpGet("patient/{personId}/{dateTime}")]
        public ActionResult GetAllForPatientByDate(int personId, DateTime dateTime)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            return Ok(MealAdapter.FromMealListToMealInfoDTOList((List<Meal>)_mealService.GetAllForPatientByDate(patient.Id, dateTime)));
        }

        [Authorize]
        [HttpGet("statistics/{personId}")]
        public ActionResult GetMealStatistics(int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            return Ok(_mealStatisticsService.GetMealsStatistics(patient.Id));
        }

        [Authorize]
        [HttpPost("add")]
        public ActionResult AddMeal(CreateMealDTO dto)
        {
            Patient patient = _patientService.getPatientByPersonId(dto.PersonId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            try
            {
                List<MealAnswer> answers = new();
                foreach (MealAnswerDTO answerDTO in dto.Answers)
                {
                    MealAnswer mealAnswer = new(_mealQuestionService.GetById(answerDTO.QuestionId), answerDTO.Answer);
                    answers.Add(mealAnswer);
                }

                float score = _mealScoreService.CalculateMealScore(answers);

                Meal meal = new(answers, score, dto.MealType, patient);
                _mealService.Create(meal);
                
                patient.UpdateHealthScore(score);
                _patientService.Update(patient);

                return StatusCode(201);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("edit")]
        public ActionResult EditMeal(CreateMealDTO dto)
        {
            Patient patient = _patientService.getPatientByPersonId(dto.PersonId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            try
            {
                Meal meal = _mealService.GetByDateAndTypeForPatient(DateTime.Today, dto.MealType, patient.Id);
                float currentScore = meal.Score;

                meal = _mealScoreService.UpdateMealScore(meal, dto.Answers);
                _mealService.Update(meal);

                if (currentScore != meal.Score)
                {
                    patient.UpdateHealthScore(meal.Score-currentScore);
                    _patientService.Update(patient);
                }

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

