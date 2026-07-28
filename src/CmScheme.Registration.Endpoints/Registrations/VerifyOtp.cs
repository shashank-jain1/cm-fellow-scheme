using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class VerifyOtp
{
    public static IEndpointRouteBuilder MapVerifyOtpEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/{applicantId:int}/verify-otp", async (
            int applicantId,
            VerifyOtpRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            VerifyMobileOtpCommand command = new VerifyMobileOtpCommand
            {
                ApplicantId = applicantId,
                MobileNumber = request.MobileNumber,
                OtpCode = request.OtpCode
            };
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("VerifyMobileOtp")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record VerifyOtpRequest(string MobileNumber, string OtpCode);
