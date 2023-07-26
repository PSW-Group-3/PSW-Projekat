using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public RoomName Number { get; set; }
        //[Range(1, 10)]

        public RoomFloor Floor { get; set; }
        public RoomType RoomType { get; set; }
        public virtual ICollection<BedDto> BedDtos { get; set; }

        public RoomDto() { }
        public RoomDto(int id, RoomName number, RoomFloor floor, RoomType roomType, ICollection<BedDto> bedDtos)
        {
            Id = id;
            Number = number;
            Floor = floor;
            RoomType = roomType;
            BedDtos = bedDtos;
        }
    }
}
