using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Registration.Endpoints.TrainingEnrollment;

public static class TrainingEnrollmentGroupExtensions
{
    public static IEndpointRouteBuilder MapTrainingEnrollmentEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/training-enrollments")
            .WithTags("Training Enrollments")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Training, "Read", requireScope: false);

        group.MapPost("", Enroll.Handle)
            .WithName("EnrollTrainee")
            .WithDisplayName("Enrol a trainee on a training schedule")
            .DisableAntiforgery();

        group.MapGet("/training-schedule/{trainingScheduleId:int}", GetEnrollments.Handle)
            .WithName("GetTrainingEnrollments")
            .WithDisplayName("List enrolments for a training schedule");

        group.MapPut("/{trainingEnrollmentId:int}/attendance", MarkAttendance.Handle)
            .WithName("MarkTrainingAttendance")
            .WithDisplayName("Mark a trainee present for a training")
            .DisableAntiforgery();

        return group;
    }
}
