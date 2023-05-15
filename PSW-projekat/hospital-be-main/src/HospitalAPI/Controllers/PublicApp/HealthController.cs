using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IDietService _dietService;
        private readonly IMealService _mealService;
        private readonly IPersonService _personService;


        public HealthController(IDietService dietService, IMealService mealService, IPersonService personService)
        {
            _dietService = dietService;
            _mealService = mealService;
            _personService = personService;
        }

        //[Authorize]
        [HttpGet("/meals/{mealType}")]
        public ActionResult GetAllMealsByType(MealType mealType)
        {
            return Ok(_mealService.GetAllMealsByType(mealType));
        }

        //[Authorize]
        [HttpPost("/meals/add")]
        public ActionResult AddMeal(MealDTO dto)
        {
            List<float> answers = new();
            foreach(AnswerDTO answerDTO in dto.Answers)
            {
                answers.Add(answerDTO.Answer);
            }

            Person person = _personService.GetById(dto.PersonId);
            if (person == null)
            {
                return BadRequest("Person not found.");
            }

            try
            {
                Meal meal = new Meal(answers, dto.MealType, person);
                _mealService.Create(meal);
                /*_healthService.UpdateHealtScore(dto.PersonId) prebaciti ovaj poziv u mealService.*/
                return StatusCode(201);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

