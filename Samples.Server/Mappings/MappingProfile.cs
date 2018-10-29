using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data = Samples.BusinessLogic.Data;

namespace Samples.Server.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
            : base()
        {
            CreateMap<Data.InventoryItem, Shared.InventoryItem>()
                .ForMember(c => c.Name, mapper => mapper.MapFrom(d => d.Inventory.Name))
                .ForMember(c => c.Description, mapper => mapper.MapFrom(d => d.Inventory.Description));

            CreateMap<Shared.InventoryItem, Data.Inventory>();
        }
    }
}
