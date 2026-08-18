using System;
using System.Security.Claims;

namespace MakeYourOwnPizza.Presentation.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idClaim == null || !Guid.TryParse(idClaim, out var id))
                throw new UnauthorizedAccessException("Invalid or missing user ID claim.");
            return id;
        }
    }
}
