using Application.DTO.Request;
using AutoMapper;
using Infrastructure.Model;

namespace Application.DTO.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<ReportRequest, Report>();
        CreateMap<UserRequest, User>();
        CreateMap<ReportImgRequest, ReportImg>();
        CreateMap<ClientRequest, Client>();
    }
}