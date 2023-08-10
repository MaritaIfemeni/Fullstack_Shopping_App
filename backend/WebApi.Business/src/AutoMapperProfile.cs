using AutoMapper;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;


namespace WebApi.Business.src
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
             CreateMap<UserCreateDto, User>();
        }
    }
}