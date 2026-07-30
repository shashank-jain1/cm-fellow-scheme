using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.UpdateProfile;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class UpdateProfile
{
    public static IEndpointRouteBuilder MapUpdateProfileEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/profile", async (
            UpdateProfileRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            UpdateProfileCommand command = new UpdateProfileCommand
            {
                ApplicantId = request.ApplicantId,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                MobileNumber = request.MobileNumber,
                EmailId = request.EmailId,
                PermanentAddress = request.PermanentAddress,
                QualificationId = request.QualificationId,
                ExperienceDetails = request.ExperienceDetails
            };
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("UpdateProfile")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record UpdateProfileRequest(
    int ApplicantId,
    string FirstName,
    string? MiddleName,
    string LastName,
    string MobileNumber,
    string EmailId,
    string PermanentAddress,
    int QualificationId,
    string? ExperienceDetails);
