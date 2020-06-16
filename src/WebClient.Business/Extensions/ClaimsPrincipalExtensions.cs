namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAuthorized(this ClaimsPrincipal principal, string roles) =>
            principal.IsAuthenticated()
            && principal.TryGetRole(out string role)
            && roles.Contains(role);
    }
}