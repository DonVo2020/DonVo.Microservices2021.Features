using AutoMapper;
using DonVo.SignalR_MediatR.Core.Models.Entities;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserResponse>();
            CreateMap<Subject, SubjectDetailResponse>();
            CreateMap<Subject, SubjectListResponse>().ForMember(dest => dest.Grade,
                opt => opt.MapFrom(src => src.Grade.Value));

            CreateMap<Grade, GradeDetailResponse>();
            CreateMap<Reminder, ReminderResponse>();
            CreateMap<Exam, ExamDetailResponse>();
            CreateMap<Exam, ExamListResponse>();
            CreateMap<Homework, HomeworkListResponse>();
            CreateMap<Homework, HomeworkDetailResponse>();
            CreateMap<Message, MessageResponse>();
            CreateMap<Reminder, ReminderResponse>();
        }
    }
}
