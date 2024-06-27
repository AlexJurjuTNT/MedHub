using AutoMapper;
using Medhub_Backend.Business.Dtos.Authentication;
using Medhub_Backend.Business.Dtos.Clinic;
using Medhub_Backend.Business.Dtos.Laboratory;
using Medhub_Backend.Business.Dtos.Patient;
using Medhub_Backend.Business.Dtos.TestRequest;
using Medhub_Backend.Business.Dtos.TestResult;
using Medhub_Backend.Business.Dtos.TestType;
using Medhub_Backend.Business.Dtos.User;
using Medhub_Backend.Domain.Entities;

namespace MedHub_Backend.WebApi;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        
        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UserRegisterRequest, User>();
        CreateMap<PatientRegisterRequest, User>();

        CreateMap<AddClinicDto, Clinic>();
        CreateMap<UpdateClinicDto, Clinic>();
        CreateMap<Clinic, ClinicDto>();

        CreateMap<AddPatientDataDto, Patient>();
        CreateMap<UpdatePatientDto, Patient>();
        CreateMap<Patient, PatientDto>();

        CreateMap<TestRequest, TestRequestDto>();
        CreateMap<UpdateTestRequestDto, TestRequest>();
        CreateMap<AddTestRequestDto, TestRequest>();

        CreateMap<AddTestResultDto, TestResult>();
        CreateMap<TestResult, AddTestResultDto>();
        CreateMap<TestResult, TestResultDto>();

        CreateMap<AddTestTypeDto, TestType>();
        CreateMap<TestTypeDto, TestType>();
        CreateMap<TestType, TestTypeDto>();

        CreateMap<LaboratoryDto, Laboratory>();
        CreateMap<CreateLaboratoryRequest, Laboratory>();
        CreateMap<Laboratory, LaboratoryDto>();
    }
}