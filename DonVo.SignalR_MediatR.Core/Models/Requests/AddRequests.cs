using System;
using System.ComponentModel.DataAnnotations;
using DonVo.SignalR_MediatR.Core.Helpers;
using DonVo.SignalR_MediatR.Core.Models.Entities;

namespace DonVo.SignalR_MediatR.Core.Models.Requests
{
    public record AddExamRequest(string Name, string Description, DateTime DueDate, int SubjectId);
    public record AddHomeworkRequest(string Name, string Description, DateTime DueDate, int SubjectId);
    public record AddReminderRequest(string Name, string Description, DateTime? DueDate, Priority Priority);
    public record AddSubjectRequest(string Name, string Description);
    public record AddMessageRequest(string RecipientEmail, string Content);
    public record AddGradeRequest(string Grade, DateTime DateSet, string Note);
}
