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
        builder.MapPost("/{applicantId:int}/send-otp", async (
            int applicantId,
            SendOtpRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            SendOtpCommand command = new SendOtpCommand
            {
                ApplicantId = applicantId,
                EmailId = request.EmailId
            };
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("SendOtp")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record SendOtpRequest(string EmailId);
