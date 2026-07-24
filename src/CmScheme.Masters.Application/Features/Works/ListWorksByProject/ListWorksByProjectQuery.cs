using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Works.ListWorksByProject;

public sealed record ListWorksByProjectQuery(int ProjectId) : IQuery<Result<List<Core.Dtos.WorkDto>>>;
