using System.Security.Claims;
using System.Text;
using guacactings.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Middleware;

public class ApiAuthorization
{
    #region Fields

    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    #endregion

    #region Constructor

    public ApiAuthorization(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    #endregion

    #region Methods

    public async Task InvokeAsync(HttpContext context, DataContext dataContext)
    {
        var authorizeAttribute = context.GetEndpoint()?.Metadata.GetMetadata<AuthorizeAttribute>();
        if (authorizeAttribute == null)
        {
            await _next(context);
            return;
        }
        
        Console.WriteLine(authorizeAttribute.Roles);
        
        var authorization = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
        {
            var token = Encoding.UTF8.GetString(Convert.FromBase64String(authorization.Substring("Bearer ".Length).Trim()));
            var claimsPrincipal = await GetClaims(dataContext, token);
            if (claimsPrincipal == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }
            
            context.User = claimsPrincipal!;

            if (authorizeAttribute.Roles!.Contains(context.User.FindFirstValue(ClaimTypes.Role)!))
            {
                await _next(context);
                return;
            }
        }
        
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }

    private async Task<ClaimsPrincipal?> GetClaims(DataContext dataContext, string token)
    {
        List<Claim> claims;
        var apiKey = _configuration.GetSection("ApiKey").Value;

        var tokenSplit = token.Split(':');
        token = tokenSplit[0];

        if (string.IsNullOrEmpty(token) || !token.Equals(apiKey))
        {
            return null;
        }

        if (tokenSplit.Length > 1)
        {
            var employee = await dataContext.Employees.Include(e => e.Administrator)
                .FirstOrDefaultAsync(e => e.Username == tokenSplit[1]);
            if (employee == null)
            {
                return null;
            }
            
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Administrator!.Id.ToString()),
                new Claim(ClaimTypes.Name, employee.Username!),
                new Claim(ClaimTypes.Role, "admin")
            };
        }
        else
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Visitor"),
                new Claim(ClaimTypes.Role, "visitor")
            };
        }

        var claimsIdentity = new ClaimsIdentity(claims, "Token");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return claimsPrincipal;
    }

    #endregion
}

public static class ApiAuthorizationExtensions
{
    public static IApplicationBuilder UseApiAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiAuthorization>();
    }
}