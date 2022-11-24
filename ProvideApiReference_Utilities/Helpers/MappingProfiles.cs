using AutoMapper;
using ProvideApiReference_Models.DTOs;
using ProvideApiReference_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_Utilities.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity,ActivityDto>().ReverseMap();
            CreateMap<Activity,PostActivityDto>().ReverseMap();
        }

    }
}
