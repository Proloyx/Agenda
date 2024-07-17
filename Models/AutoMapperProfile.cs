using System.Net.NetworkInformation;
using Agenda.Data;
using Agenda.Models.UserModels;
using AutoMapper;

namespace Agenda.Models
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile(){
            CreateMap<UserRegister,User>();
        }
    }
}