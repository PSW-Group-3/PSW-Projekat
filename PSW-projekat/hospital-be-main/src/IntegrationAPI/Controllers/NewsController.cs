using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.Newses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntegrationAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return
                     Ok(_newsService.GetAll());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pending")]
        public ActionResult GetAllPending()
        {
            try
            {
                return Ok(_newsService.GetAllPending());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Update(News news)
        {
            try
            {
                _newsService.Update(news); 
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                return Ok(_newsService.GetById(id));
            }catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}
