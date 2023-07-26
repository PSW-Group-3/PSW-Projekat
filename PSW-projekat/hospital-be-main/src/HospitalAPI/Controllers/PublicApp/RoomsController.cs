using System.Collections.Generic;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //[Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService iroomService)
        {
            _roomService = iroomService;
        }

        // GET: api/rooms
        [HttpGet]
        public ActionResult GetAll()
        {
            List<RoomDto> roomDtos = new List<RoomDto>();

            foreach (var room in _roomService.GetAll()) 
            {
                if (room.RoomType.Equals(RoomType.storage)) continue;


                List<BedDto> bedDtos = new List<BedDto>();

                foreach (Bed bed in room.Beds) 
                {

                    if (bed.Patient == null) 
                    {
                        BedDto bedDtoNull = new BedDto(bed.Id, bed.Name, bed.BedState,
                          null, bed.Quantity);

                        bedDtos.Add(bedDtoNull);
                        continue;
                    } 

                    PatientDto patientDto = new PatientDto(bed.Patient.Id, bed.Patient.Person.Name,
                    bed.Patient.Person.Surname, bed.Patient.Person.Email.ToString(), bed.Patient.Person.Role);

                    BedDto bedDto = new BedDto(bed.Id, bed.Name, bed.BedState, patientDto, bed.Quantity);

                    bedDtos.Add(bedDto);

                }

                RoomDto roomDto = new RoomDto(room.Id, room.Number, room.Floor, room.RoomType, bedDtos);   
                roomDtos.Add(roomDto);
            }

            return Ok(roomDtos);
        }

        // GET api/rooms/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var room = _roomService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // POST api/rooms
        [HttpPost]
        public ActionResult Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _roomService.Create(room);
            return Ok();
        }

        // PUT api/rooms/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Id)
            {
                return BadRequest();
            }

            try
            {
                _roomService.Update(room);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(room);
        }

        // DELETE api/rooms/2
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var room = _roomService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            _roomService.Delete(room);
            return NoContent();
        }

        [HttpGet("room/{roomId}")]
        public ActionResult GetAllBedsByRoom(int roomId)
        {
            return Ok(_roomService.GetAllBedsByRoom(roomId));
        }

        [HttpGet("room/medicines")]
        public ActionResult GetAllStorageMedicnes()
        {
            return Ok(_roomService.GetAllStorageMedicnes());
        }

        [HttpGet("room/bloods")]
        public ActionResult GetAllStorageBloods()
        {
            return Ok(_roomService.GetAllStorageBloods());
        }


    }
}
