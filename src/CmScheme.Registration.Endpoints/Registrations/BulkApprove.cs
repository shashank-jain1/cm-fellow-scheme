using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.BulkApprove;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class BulkApprove
{
    public static IEndpointRouteBuilder MapBulkApproveEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/bulk-approve", async (
            BulkApproveRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            BulkApproveRegistrationCommand command = new BulkApproveRegistrationCommand
            {
                ApplicantIds = request.ApplicantIds,
                Action = request.Action,
                Remarks = request.Remarks,
                PerformedBy = request.PerformedBy
            };
            ValueTask<Result<int>> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("BulkApproveRegistration")
        .Produces<int>(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record BulkApproveRequest(
    IReadOnlyList<int> ApplicantIds,
    string Action,
    string? Remarks,
    int PerformedBy);
