using System.Security.Claims;
using Blog.Models;

namespace Blog.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Nome)
        };
        result.AddRange(
            user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Nome))
        );
        return result;
    }
}