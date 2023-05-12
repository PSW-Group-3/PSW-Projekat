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


        public HealthController(IDietService dietService)
        {
            _dietService = dietService;
        }

        //[Authorize]
        [HttpGet("/meals/{mealType}")]
        public ActionResult GetAllMealsByType(MealType mealType)
        {
            return Ok(_mealService.GetAllMealsByType(mealType));
        }

        //[Authorize]
        [HttpPost("/meals/add/breakfast")]
        public ActionResult AddBreakfast(MealDTO dto)
        {
            List<int> answers = new();
            foreach(AnswerDTO answerDTO in dto.Answers)
            {
                answers.Add(answerDTO.Answer);
            }

            Meal meal = new Meal(answers, MealType.breakfast);

            try
            {
                _mealService.Create(meal);
                /*_healthService.UpdateHealtScore(dto.PersonId)*/
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

