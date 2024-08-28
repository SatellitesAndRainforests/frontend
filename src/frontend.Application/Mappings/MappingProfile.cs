using AutoMapper;
using frontend.Domain.Models;
using frontend.Domain.Responses;

namespace frontend.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {


            CreateMap<TestResonpse, TestModel>();


        }



    }
}
