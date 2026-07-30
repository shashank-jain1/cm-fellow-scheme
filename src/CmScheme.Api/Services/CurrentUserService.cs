using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CmScheme.Common.Core.Services;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Api.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserAccountId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)
                ?? _httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub);
            return claim != null && int.TryParse(claim.Value, out var id) ? id : null;
        }
    }

    public string? Username =>
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value
        ?? _httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;

    public string? Role =>
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

    public bool IsAdmin => Role == "Admin";
}
