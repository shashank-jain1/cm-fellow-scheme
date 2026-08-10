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
        builder.MapPut("/verify-otp", async (
            VerifyOtpRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            VerifyMobileOtpCommand command = new VerifyMobileOtpCommand
            {
                MobileNumber = request.MobileNumber,
                OtpCode = request.OtpCode,
            };
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("VerifyMobileOtp")
        .WithDisplayName("Verify a mobile OTP")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record VerifyOtpRequest(string MobileNumber, string OtpCode);
