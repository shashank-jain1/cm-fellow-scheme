using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.ApproveRegistration;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class Approve
{
    public static IEndpointRouteBuilder MapApproveEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/{applicantId:int}/approve", async (
            int applicantId,
            ApproveRegistrationRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            ApproveRegistrationCommand command = new ApproveRegistrationCommand(applicantId, request.ApprovedBy);
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("ApproveRegistration")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record ApproveRegistrationRequest(int ApprovedBy);
