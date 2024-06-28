using AutoMapper;
using Medhub_Backend.Application.Dtos.Authentication;
using Medhub_Backend.Application.Dtos.Clinic;
using Medhub_Backend.Application.Dtos.Laboratory;
using Medhub_Backend.Application.Dtos.Patient;
using Medhub_Backend.Application.Dtos.TestRequest;
using Medhub_Backend.Application.Dtos.TestResult;
using Medhub_Backend.Application.Dtos.TestType;
using Medhub_Backend.Application.Dtos.User;
using Medhub_Backend.Domain.Entities;
using CreateTestRequestRequest = Medhub_Backend.Application.Dtos.TestRequest.CreateTestRequestRequest;

namespace MedHub_Backend.WebApi;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UserRegisterRequest, User>();
        CreateMap<PatientRegisterRequest, User>();

        CreateMap<Clinic, ClinicDto>();
        CreateMap<AddClinicRequest, Clinic>();
        CreateMap<UpdateClinicRequest, Clinic>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Patient, PatientDto>();
        CreateMap<UpdatePatientRequest, Patient>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<AddPatientInformationRequest, Patient>();

        CreateMap<TestRequest, TestRequestDto>();
        CreateMap<UpdateTestRequestDto, TestRequest>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<CreateTestRequestRequest, TestRequest>();

        CreateMap<TestResult, TestResultDto>();
        CreateMap<CreateTestResultRequest, TestResult>();

        CreateMap<TestType, TestTypeDto>();
        CreateMap<UpdateTestTypeRequest, TestType>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<CreateTestTypeRequest, TestType>();


        CreateMap<Laboratory, LaboratoryDto>();
        CreateMap<UpdateLaboratoryRequest, Laboratory>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<CreateLaboratoryRequest, Laboratory>();
    }
}