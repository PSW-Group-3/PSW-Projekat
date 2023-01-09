using AutoMapper;
using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.Tender;

namespace IntegrationAPI.Adapters
{
    public class BidAdapter : Profile
    {
        public BidAdapter()
        {
            CreateMap<BidDTO, Bid>().ReverseMap();
            CreateMap<BidDTO, Bid>();
        }
    }
}
