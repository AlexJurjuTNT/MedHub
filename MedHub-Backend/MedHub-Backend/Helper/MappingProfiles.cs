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

        CreateMap<AddClinicDto, Clinic>();
        CreateMap<Clinic, ClinicDto>();
        CreateMap<ClinicDto, Clinic>();

        CreateMap<AddPatientDataDto, Patient>();
        CreateMap<Patient, PatientDto>();

        CreateMap<TestRequest, TestRequestDto>();
        CreateMap<TestRequestDto, TestRequest>();
        CreateMap<AddTestRequestDto, TestRequest>();

        CreateMap<AddTestResultDto, TestResult>();
        CreateMap<TestResult, AddTestResultDto>();
        CreateMap<TestResult, TestResultDto>();

        CreateMap<AddTestTypeDto, TestType>();
        CreateMap<TestTypeDto, TestType>();
        CreateMap<TestType, TestTypeDto>();
    }
}