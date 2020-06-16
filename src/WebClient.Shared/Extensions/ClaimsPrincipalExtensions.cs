using System.Linq;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get the principal identity IsAuthenticated property.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool IsAuthenticated(this ClaimsPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Get the role of the principal or null.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>The role or null.</returns>
        public static string GetRole(this ClaimsPrincipal principal)
        {
            return principal.Claims.Where(x => x.Type == ClaimTypes.Role).SingleOrDefault()?.Value;
        }

        /// <summary>
        /// Try to get the role of the principal.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>True if there is a role.</returns>
        public static bool TryGetRole(this ClaimsPrincipal principal, out string role)
        {
            role = principal.Claims.Where(x => x.Type == ClaimTypes.Role).SingleOrDefault()?.Value;
            return !(role is null);
        }

        /// <summary>
        /// Get the sub claim or null.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>The id or null.</returns>
        public static string GetId(this ClaimsPrincipal principal)
        {
            return principal.Claims.Where(x => x.Type == "sub").SingleOrDefault()?.Value;
        }

        /// <summary>
        /// Try to get the sub of the principal.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>True if there is an id.</returns>
        public static bool TryGetId(this ClaimsPrincipal principal, out string id)
        {
            id = principal.Claims.Where(x => x.Type == "sub").SingleOrDefault()?.Value;
            return !(id is null);
        }

        /// <summary>
        /// Get the store_id claim of the principal or null.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>The store_id or null.</returns>
        public static string GetStoreId(this ClaimsPrincipal principal)
        {
            return principal.Claims.Where(x => x.Type == "store_id").SingleOrDefault()?.Value;
        }

        /// <summary>
        /// Try to get the store_id of the principal.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>True if there is a store_id.</returns>
        public static bool TryGetStoreId(this ClaimsPrincipal principal, out string storeId)
        {
            storeId = principal.Claims.Where(x => x.Type == "store_id").SingleOrDefault()?.Value;
            return !(storeId is null);
        }
    }
}