using AutoMapper;
using desafio.Models.Entites;
using desafio.Models.RequestModels;

namespace desafio.Data.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles() {
            CreateMap<AddUserRequestModel, User>()
                .ReverseMap();
        }
    }
}