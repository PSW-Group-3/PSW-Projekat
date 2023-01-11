using IntegrationLibrary.Core.Model.Tender;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO
{
    public class TenderDTO : BaseModelDTO
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public List<DemandDTO> Demands { get; set; }

        [Required]
        public TenderState State { get; set; }
    }
}
