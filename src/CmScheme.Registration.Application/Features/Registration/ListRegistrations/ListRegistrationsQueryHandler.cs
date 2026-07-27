using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Dtos;

namespace CmScheme.Registration.Application.Features.Registration.ListRegistrations;

public sealed class ListRegistrationsQueryHandler(IRegistrationQueryDbContext dbContext)
    : IQueryHandler<ListRegistrationsQuery, Result<List<RegistrationListItem>>>
{
    public async ValueTask<Result<List<RegistrationListItem>>> Handle(
        ListRegistrationsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.Applicant> query = dbContext.Applicants;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searchTerm = request.SearchTerm.ToLower();
            query = query.Where(a =>
                a.FirstName.ToLower().Contains(searchTerm) ||
                a.LastName.ToLower().Contains(searchTerm) ||
                a.MobileNumber.Contains(searchTerm) ||
                a.EmailId.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            query = query.Where(a => a.Status == request.Status);
        }

        int pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        List<RegistrationListItem> items = await query
            .OrderByDescending(a => a.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new RegistrationListItem(
                a.ApplicantId,
                a.FirstName,
                a.LastName,
                a.MobileNumber,
                a.EmailId,
                a.DateOfBirth,
                a.Status,
                a.CreatedOn))
            .ToListAsync(cancellationToken);

        return Result<List<RegistrationListItem>>.Success(items);
    }
}
