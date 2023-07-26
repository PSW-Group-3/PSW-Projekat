using IntegrationAPI.Adapters;
using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Service.Tenders;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntegrationAPI.Controllers
{
    //[Authorize(Roles = "Manager")]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : Controller
    {
        private readonly ITenderService _tenderService;

        public TenderController(ITenderService tenderService)
        {
            _tenderService = tenderService;
        }

        [HttpPost]
        public ActionResult Create(TenderDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Tender tender = TenderAdapter.FromDTO(entity);
            try
            {
                _tenderService.Create(tender);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Tender tender = _tenderService.GetById(id);
                if (tender == null)
                {
                    return NotFound();
                }

                return Ok(tender);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("open")]
        public ActionResult GetAllOpen()
        {
            try
            {
                return Ok(_tenderService.GetAllOpen());
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
                return Ok(_tenderService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CloseTender")]
        public ActionResult CloseTender(int tenderId, int winningBidId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _tenderService.CloseTenderWithWinner(tenderId, winningBidId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Bid/{tenderID}")]
        public ActionResult BidOnTender(int tenderID, BidDTO bidDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Bid bid = new Bid(bidDTO.DeliveryDate, bidDTO.Price, bidDTO.BloodBankId);
                _tenderService.BidOnTender(tenderID, bid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Blood/{Start}/{End}")]
        public ActionResult CreateStatisticsOfBloodType(DateTime Start, DateTime End)
        {
            try 
            {
                return Ok(_tenderService.CreateStatisticsOfBloodType(Start, End));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("BloodBanks/{Start}/{End}")]
        public ActionResult GetBloodBankWinners(DateTime Start, DateTime End)
        {
            try
            {
                return Ok(_tenderService.GetBloodBankWinners(Start, End));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("BloodBanksStatistics/{Start}/{End}")]
        public ActionResult CreateStatisticsOfBloodBank(DateTime Start, DateTime End)
        {
            try
            {
                return Ok(_tenderService.CreateStatisticsOfBloodBank(Start, End));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
