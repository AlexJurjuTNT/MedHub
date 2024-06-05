using AutoMapper;
using MedHub_Backend.Dto;
using MedHub_Backend.Model;

namespace MedHub_Backend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<UserRegisterDto, User>();

        CreateMap<Clinic, ClinicDto>();
        CreateMap<ClinicDto, Clinic>();

        CreateMap<PatientDto, Patient>();
        CreateMap<PatientDto, Patient>();

        CreateMap<TestRequest, TestRequestDto>();
        CreateMap<TestRequestDto, TestRequest>();

        CreateMap<TestTypeDto, TestType>();
        CreateMap<TestType, TestTypeDto>();
    }
}