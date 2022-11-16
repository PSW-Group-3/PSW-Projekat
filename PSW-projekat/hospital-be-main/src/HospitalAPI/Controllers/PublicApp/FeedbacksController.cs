using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
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
    public class FeedbacksController : ControllerBase
    {    
        private readonly FeedbackService _feedbackService;
        private readonly PersonService _userService;

        public FeedbacksController(FeedbackService feedbackService, PersonService userService)
        {
            _feedbackService = feedbackService;
            _userService = userService;
        }

        [HttpGet("/real")]
        public ActionResult GetAll()
        {
            return Ok(_feedbackService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var feedback = _feedbackService.GetById(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        [HttpPost]
        public ActionResult Create(CreateFeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person user = _userService.GetById(int.Parse(feedbackDto.UserId));

            Feedback feedback = new Feedback {
                Description = feedbackDto.Description,
                IsAnonimous = feedbackDto.IsAnonimous,
                IsPublic = feedbackDto.IsPublic,
                DateCreated = DateTime.Now,
                User = user,
                Status = FeedbackStatus.Pending
            };

            _feedbackService.Create(feedback);
            return CreatedAtAction("GetById", new { id = feedback.Id }, feedback);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedback.Id)
            {
                return BadRequest();
            }

            try
            {
                _feedbackService.Update(feedback);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(feedback);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var room = _feedbackService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            _feedbackService.Delete(room);
            return NoContent();
        }

        [HttpGet("public")]
        public ActionResult GetAllFeedbackPublicDtos()
        {
            return Ok(_feedbackService.GetAllFeedbackPublicDtos());
        }
    }
}
