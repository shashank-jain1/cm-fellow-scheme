using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.RejectRegistration;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class Reject
{
    public static IEndpointRouteBuilder MapRejectEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/{applicantId:int}/reject", async (
            int applicantId,
            RejectRegistrationRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            RejectRegistrationCommand command = new RejectRegistrationCommand { ApplicantId = applicantId, Reason = request.Reason, RejectedBy = request.RejectedBy };
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("RejectRegistration")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record RejectRegistrationRequest(string Reason, int RejectedBy);
