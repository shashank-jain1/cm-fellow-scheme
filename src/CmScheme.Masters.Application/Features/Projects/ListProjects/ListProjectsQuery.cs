using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Projects.ListProjects;

public sealed record ListProjectsQuery : IQuery<Result<List<Core.Dtos.ProjectDto>>>;
