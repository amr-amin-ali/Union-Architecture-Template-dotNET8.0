using AutoMapper;
using Elearning.Entittes.Models;
using Elearning.Shared.DTOs;

namespace Elearning.Shared.AutoMapperProfiles
{
    public class OTPDataProfile:Profile
    {
        public OTPDataProfile()
        {
            CreateMap<OTPDataDTO,OTPData>().ReverseMap();
        }
    }
}
