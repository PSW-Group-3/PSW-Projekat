using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class SymptomController : ControllerBase
    {

        private readonly ISymptomService _symptomService;

        public SymptomController(ISymptomService symptomService)
        {
            _symptomService = symptomService;

        }


        [HttpGet]
        public ActionResult GetAll()
        {
            List<Symptom> simptomi = new List<Symptom>();
            foreach (var simptom in _symptomService.GetAll())
            {
                simptomi.Add(simptom);
            }

            return Ok(simptomi);
        }


    }
}
