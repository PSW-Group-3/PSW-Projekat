using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Model
{
    public class Room : BaseModel
    {
        public RoomName Number { get; set; }
        //[Range(1, 10)]
        public RoomFloor Floor { get; set; }
        public RoomType RoomType { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }
        public virtual ICollection<Blood> Bloods { get; set; }
        public virtual ICollection<Bed> Beds { get; set; }


    }
}
