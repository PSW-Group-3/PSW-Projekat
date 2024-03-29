﻿using AutoMapper;
using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.EmergencyBloodRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IntegrationAPI.Controllers
{
    //[Authorize(Roles = "Manager")]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyBloodRequestController : ControllerBase
    {
        private readonly IEmergencyBloodRequestService _emergencyBloodRequestService;
        private readonly IMapper _mapper;

        public EmergencyBloodRequestController(IEmergencyBloodRequestService emergencyBloodRequestService, IMapper mapper)
        {
            _emergencyBloodRequestService = emergencyBloodRequestService;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult RequestEmergencyBlood(EmergencyBloodRequestDTO requestDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            EmergencyBloodRequestGRPC request = _mapper.Map<EmergencyBloodRequestGRPC>(requestDTO);
            if(request == null)
            {
                return BadRequest("Error when mapping dto to entity");
            }
            try
            {
                _emergencyBloodRequestService.RequestEmergencyBlood(request);
                return Ok(requestDTO);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_emergencyBloodRequestService.GetAll());
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
