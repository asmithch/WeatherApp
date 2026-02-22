using AutoMapper;
using AuthenticationService.Application.DTOs;
using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequestDto, User>();
    }
}