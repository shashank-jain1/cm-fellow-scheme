using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.SendOtp;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class SendOtp
{
    public static IEndpointRouteBuilder MapSendOtpEndpoint(this IEndpointRouteBuilder builder)
    {
        // Keyed on the mobile number, not an applicant id: OTP verification happens
        // during the wizard, before the applicant record is created.
        builder.MapPost("/send-otp", async (
            SendOtpRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            SendOtpCommand command = new SendOtpCommand
            {
                MobileNumber = request.MobileNumber,
            };
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("SendOtp")
        .WithDisplayName("Send a mobile OTP")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record SendOtpRequest(string MobileNumber);
