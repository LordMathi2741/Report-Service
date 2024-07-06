using Application.DTO.Responses;
using AutoMapper;
using Infrastructure.Model;

namespace Application.DTO.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<User, UserResponse>();
        CreateMap<Report, ReportResponse>();
    }
    
}