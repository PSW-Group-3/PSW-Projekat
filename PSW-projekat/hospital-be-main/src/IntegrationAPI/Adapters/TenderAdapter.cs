using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model.Tender;
using System.Collections.Generic;

namespace IntegrationAPI.Adapters
{
    public class TenderAdapter
    {
        public static Tender FromDTO(TenderDTO entity)
        {
            List<Demand> demands = new List<Demand>();
            
            foreach(DemandDTO demand in entity.Demands)
                demands.Add(DemandAdapter.FromDTO(demand));

            return new Tender(entity.DueDate, demands, entity.State);
        }

        public static TenderDTO ToDTO(Tender entity)
        {
            List<DemandDTO> demands = new List<DemandDTO>();

            foreach (Demand demand in entity.Demands)
                demands.Add(DemandAdapter.ToDTO(demand));

            return new TenderDTO()
            {
                DueDate = entity.DueDate,
                Demands = demands,
                State = entity.State,
                Id = entity.Id,
            };
        }
    }
}
