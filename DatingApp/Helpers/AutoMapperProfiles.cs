using AutoMapper;
using DatingApp.DTO;
using DatingApp.Entities;
using DatingApp.Extensions;

namespace DatingApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(Src => Src.DateofBirth.CalculateAge()))
                .ForMember(dest => dest.PhotoUrl, opt => 
                opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain)!.Url));
            CreateMap<Photo, PhotoDto>();
        }
    }
}
