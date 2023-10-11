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
        private readonly IMealStatisticsService _mealStatisticsService;
        private readonly IMealQuestionService _mealQuestionService;
        private readonly IMealScoreService _mealScoreService;

        private readonly IPatientService _patientService;
        private readonly IPatientScoreService _patientScoreService;

        public MealController(IMealService mealService, IMealStatisticsService mealStatisticsService, IMealQuestionService mealQuestionService, IPatientService patientService, IMealScoreService mealScoreService, IPatientScoreService patientScoreService)
        {
            _mealService = mealService;
            _mealStatisticsService = mealStatisticsService;
            _mealQuestionService = mealQuestionService;
            _mealScoreService = mealScoreService;
            _patientService = patientService;
            _patientScoreService = patientScoreService;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("mealquestions/{mealType}")]
        public ActionResult GetAllMealQuestionsByType(MealType mealType)
        {
            return Ok(MealQuestionAndAnswerAdapter.FromMealQuestionListToMealQuestionDTOList(_mealQuestionService.GetAllMealQuestionsByMealType(mealType) as List<MealQuestion>));
        }

        [Authorize(Roles = "Patient")]
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

        [Authorize(Roles = "Patient")]
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

        [Authorize(Roles = "Patient")]
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
                    answers.Add(MealQuestionAndAnswerAdapter.FromMealAnswerDTOtoMealAnswer(_mealQuestionService.GetById(answerDTO.QuestionId), answerDTO.Answer));
                }

                float score = _mealScoreService.CalculateMealScore(answers);

                _mealService.Create(new(answers, score, dto.MealType, patient));
                
                _patientService.Update(_patientScoreService.UpdatePatientHealtScore(patient, score));

                return StatusCode(201);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Patient")]
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
                    _patientService.Update(_patientScoreService.UpdatePatientHealtScore(patient, meal.Score - currentScore));
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

