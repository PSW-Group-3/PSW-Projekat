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

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly MealStatisticsService _mealStatisticsService;
        private readonly IMealAnswerService _mealAnswerService;
        private readonly IMealQuestionService _mealQuestionService;
        private readonly IPersonService _personService;
        private readonly IPatientService _patientService;


        public MealController(IMealService mealService, MealStatisticsService mealStatisticsService, IMealAnswerService mealAnswerService, IMealQuestionService mealQuestionService, IPersonService personService, IPatientService patientService)
        {
            _mealService = mealService;
            _mealStatisticsService = mealStatisticsService;
            _mealAnswerService = mealAnswerService;
            _mealQuestionService = mealQuestionService;
            _personService = personService;
            _patientService = patientService;
        }

        //[Authorize]
        [HttpGet("all/{mealType}")]
        public ActionResult GetAllMealsByType(MealType mealType)
        {
            return Ok(_mealService.GetAllMealsByType(mealType));
        }

        //[Authorize]
        [HttpGet("mealquestions/{mealType}")]
        public ActionResult GetAllMealQuestionsByType(MealType mealType)
        {
            return Ok(MealQuestionAdapter.ToListDTO((List<MealQuestion>)_mealQuestionService.GetAllMealQuestionsByMealType(mealType)));
        }

        //[Authorize]
        [HttpGet("patient/{patientId}/{dateTime}")]
        public ActionResult GetMealsForPatientByDate(int patientId, DateTime dateTime)
        {
            return Ok(MealAdapter.ToListInfoDTO((List<Meal>)_mealService.GetMealsForPatientByDate(patientId, dateTime)));
        }

        //[Authorize]
        [HttpGet("statistics/{patientId}")]
        public ActionResult GetMealStatistics(int patientId)
        {
            return Ok(_mealStatisticsService.GetMealsStatistics(patientId));
        }

        //[Authorize]
        [HttpPost("add")]
        public ActionResult AddMeal(MealDTO dto)
        {
            Person person = _personService.GetById(dto.PersonId);
            if (person == null)
            {
                return BadRequest("Person not found.");
            }

            try
            {
                Meal meal = new Meal(dto.Answers, dto.MealType, person);
                _mealService.Create(meal);
                foreach (MealAnswerDTO answerDTO in dto.Answers)
                {
                    MealAnswer mealAnswer = new MealAnswer(_mealQuestionService.GetById(answerDTO.QuestionId), meal, answerDTO.Answer);
                    _mealAnswerService.Create(mealAnswer);
                }
                Patient patient = _patientService.getPatientByPersonId(person.Id);
                patient.UpdateHealthScore(meal.Score);
                _patientService.Update(patient);
                return StatusCode(201);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize]
        [HttpPut("edit")]
        public ActionResult EditMeal(MealDTO dto)
        {
            Person person = _personService.GetById(dto.PersonId);
            if (person == null)
            {
                return BadRequest("Person not found.");
            }

            try
            {
                Meal meal = _mealService.GetByDateAndType(DateTime.Today, dto.MealType);
                float currentScore = meal.Score;
                meal.CalculateScore(dto.Answers);
                _mealService.Update(meal);
                foreach (MealAnswerDTO answerDTO in dto.Answers)
                {
                    MealAnswer mealAnswer = _mealAnswerService.GetMealAnswerForMealByQuestionId(meal, answerDTO.QuestionId);
                    mealAnswer.Answer = answerDTO.Answer;
                    _mealAnswerService.Update(mealAnswer);
                }
                if(currentScore != meal.Score)
                {
                    Patient patient = _patientService.getPatientByPersonId(person.Id);
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

