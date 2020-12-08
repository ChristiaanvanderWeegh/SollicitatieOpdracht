using Microsoft.AspNetCore.Authorization;

namespace Api.AuthRequirement
{
    public class JwtRequirement : IAuthorizationRequirement
    {
        public string Client_Secret { get; set; }
    }
}
