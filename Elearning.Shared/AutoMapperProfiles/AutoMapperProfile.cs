namespace Elearning.Shared.AutoMapperProfiles
{
    using AutoMapper;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>() {
              new OTPDataProfile()
            }));

        }
    }
}
