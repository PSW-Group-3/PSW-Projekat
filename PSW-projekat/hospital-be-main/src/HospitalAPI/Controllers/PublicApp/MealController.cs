using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AutoMapper;
using HospitalAPI.Adapters;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly IMealAnswerService _mealAnswerService;
        private readonly IMealQuestionService _mealQuestionService;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;


        public MealController(IMealService mealService, IMealAnswerService mealAnswerService, IMealQuestionService mealQuestionService, IPersonService personService, IMapper mapper)
        {
            _mealService = mealService;
            _mealAnswerService = mealAnswerService;
            _mealQuestionService = mealQuestionService;
            _personService = personService;
            _mapper = mapper;

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
        [HttpGet("patient/{patientId}")]
        public ActionResult GetMealsForPatient(int patientId)
        {
            return Ok(MealAdapter.ToListInfoDTO((List<Meal>)_mealService.GetMealsForPatient(patientId)));
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
                /*_healthService.UpdateHealtScore(dto.PersonId) prebaciti ovaj poziv u mealService.*/
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
                meal.Score = meal.CalculateScore(dto.Answers);
                _mealService.Update(meal);
                foreach (MealAnswerDTO answerDTO in dto.Answers)
                {
                    MealAnswer mealAnswer = _mealAnswerService.GetMealAnswerForMealByQuestionId(meal, answerDTO.QuestionId);
                    mealAnswer.Answer = answerDTO.Answer;
                    _mealAnswerService.Update(mealAnswer);
                }
                /*_healthService.UpdateHealtScore(dto.PersonId) prebaciti ovaj poziv u mealService.*/
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

