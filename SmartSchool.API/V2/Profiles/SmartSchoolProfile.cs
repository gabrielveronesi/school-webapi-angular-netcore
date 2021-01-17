using AutoMapper;
using SmartSchool.API.V2.Dtos;
using SmartSchool.API.Models;
using SmartSchool.API.Helpers;

namespace SmartSchool.API.V2.Helpers
{

    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>() //Toda vez que eu estiver trabalhando com Aluno. eu quero que meu aluno seja mapeado pelo AlunoDTO
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge()) 
                );

                CreateMap<AlunoDto, Aluno>();
                CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

        }
    }
}