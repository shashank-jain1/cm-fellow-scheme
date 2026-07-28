using Ardalis.Result;
using CmScheme.Certificate.Application.Features.Certificate.GetCertificateStatus;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class Download
{
    public static async Task<IResult> Handle(int certificateId, ISender sender)
    {
        GetCertificateStatusQuery query = new GetCertificateStatusQuery
        {
            CertificateId = certificateId
        };
        Result<Core.Dtos.CertificateApplicationDto?> result = await sender.Send(query);

        if (!result.IsSuccess || result.Value is null)
        {
            return Results.NotFound("Certificate not found.");
        }

        string? pdfPath = result.Value.CertificatePdfPath;
        if (string.IsNullOrEmpty(pdfPath))
        {
            return Results.BadRequest("Certificate PDF has not been generated yet.");
        }

        string fullPath = Path.Combine("wwwroot", pdfPath.TrimStart('/'));
        if (!File.Exists(fullPath))
        {
            return Results.NotFound("Certificate PDF file not found on server.");
        }

        byte[] fileBytes = await File.ReadAllBytesAsync(fullPath);
        return Results.File(fileBytes, "application/pdf", Path.GetFileName(fullPath));
    }
}
