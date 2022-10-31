﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.PrivateApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateFeedbacksController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public PrivateFeedbacksController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public ActionResult GetAllFeedbackDtos()
        {
            return Ok(_feedbackService.GetAllFeedbackDtos());
        }
    }
}