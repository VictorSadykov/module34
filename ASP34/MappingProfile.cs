using ASP34.Configuration;
using ASP34.Contracts.Models.Devices;
using ASP34.Contracts.Models.Home;
using ASP34.Contracts.Models.Rooms;
using ASP34.Data.Models;
using AutoMapper;

namespace ASP34
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo,
                    opt => opt.MapFrom(src => src.Address));

            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location,
                    opt => opt.MapFrom(r => r.RoomLocation));
            CreateMap<AddRoomRequest, Room>();
            CreateMap<Device, DeviceView>();
            CreateMap<Room, RoomView>();
        }
    }
}
