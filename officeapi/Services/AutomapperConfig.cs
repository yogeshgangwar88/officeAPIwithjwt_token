using AutoMapper;
using ServiceLibrary.Models;
using ServiceLibrary.Models.DTOs;

namespace officeapi.Services
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Login, LoginDTO>().ReverseMap();
        }
        
    }
}
