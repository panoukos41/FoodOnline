using OpenIddict.Abstractions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Tries to return the claim of type Subject that contains the user Id.
        /// If it's not found null is returned.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The id of the user or null if not found.</returns>
        public static string GetSubject(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type == OpenIddictConstants.Claims.Subject)?.Value;
        }

        /// <summary>
        /// Tries to return the claim of type Subject that contains the user Id.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id">If this returns true the user id will be populated.</param>
        /// <returns>True if the user had an id otherwise false.</returns>
        public static bool TryGetSubject(this ClaimsPrincipal user, [MaybeNullWhen(false)] out string id)
        {
            var userid = user.GetSubject();
            if (userid != null)
            {
                id = userid;
                return true;
            }
            id = null;
            return false;
        }
    }
}