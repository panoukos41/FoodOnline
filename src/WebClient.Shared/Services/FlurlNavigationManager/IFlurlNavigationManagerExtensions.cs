using Flurl;
using System.Collections.Generic;

namespace FoodOnline.WebClient.Services
{
    public static class IFlurlNavigationManagerExtensions
    {
        /// <summary>
        /// Appends a segment to the URL path, ensuring there is one and only one '/' character as a seperator.
        /// </summary>
        /// <param name="segment">The segment to append</param>
        /// <param name="fullyEncode">If true, URL-encodes reserved characters such as '/', '+', and '%'. Otherwise,
        /// only encodes strictly illegal characters (including '%' but only when not followed
        /// by 2 hex characters).</param>
        /// <returns>the Url object with the segment appended</returns>
        /// <exception cref="System.ArgumentNullException">Segment is null.</exception>
        public static IFlurlNavigationManager AppendPathSegment(this IFlurlNavigationManager manager, object segment, bool fullyEncode = false)
        {
            manager.Url.AppendPathSegment(segment, fullyEncode);
            return manager;
        }

        /// <summary>
        /// Appends multiple segments to the URL path, ensuring there is one and only one
        /// '/' character as a seperator.
        /// </summary>
        /// <param name="segments">The segments to append</param>
        /// <returns>The Url object with the segments appended</returns>
        public static IFlurlNavigationManager AppendPathSegments(this IFlurlNavigationManager manager, params object[] segments)
        {
            manager.Url.AppendPathSegments(segments);
            return manager;
        }

        /// <summary>
        /// Appends multiple segments to the URL path, ensuring there is one and only one
        /// '/' character as a seperator.
        /// </summary>
        /// <param name="segments">The segments to append</param>
        /// <returns>The Url object with the segments appended</returns>
        public static IFlurlNavigationManager AppendPathSegments(this IFlurlNavigationManager manager, IEnumerable<object> segments)
        {
            manager.Url.AppendPathSegments(segments);
            return manager;
        }

        /// <summary>
        /// Removes the URL fragment including the #.
        /// </summary>
        /// <returns>The Url object with the fragment removed</returns>
        public static IFlurlNavigationManager RemoveFragment(this IFlurlNavigationManager manager)
        {
            manager.Url.RemoveFragment();
            return manager;
        }

        /// <summary>
        /// Removes a name/value pair from the query by name.
        /// </summary>
        /// <param name="name">Query string parameter name to remove</param>
        /// <returns>The Url object with the query parameter removed</returns>
        public static IFlurlNavigationManager RemoveQueryParam(this IFlurlNavigationManager manager, string name)
        {
            manager.Url.RemoveQueryParam(name);
            return manager;
        }

        /// <summary>
        /// Removes multiple name/value pairs from the query by name.
        /// </summary>
        /// <param name="names">Query string parameter names to remove</param>
        /// <returns>The Url object with the query parameters removed</returns>
        public static IFlurlNavigationManager RemoveQueryParams(this IFlurlNavigationManager manager, IEnumerable<string> names)
        {
            manager.Url.RemoveQueryParams(names);
            return manager;
        }

        /// <summary>
        /// Removes multiple name/value pairs from the query by name.
        /// </summary>
        /// <param name="names">Query string parameter names to remove</param>
        /// <returns>The Url object with the query parameters removed</returns>
        public static IFlurlNavigationManager RemoveQueryParams(this IFlurlNavigationManager manager, params string[] names)
        {
            manager.Url.RemoveQueryParams(names);
            return manager;
        }

        /// <summary>
        /// Resets the URL to its root, including the scheme, any user info, host, and port
        /// (if specified).
        /// </summary>
        /// <returns>The Url object trimmed to its root.</returns>
        public static IFlurlNavigationManager ResetToRoot(this IFlurlNavigationManager manager)
        {
            manager.Url.ResetToRoot();
            return manager;
        }

        /// <summary>
        /// Set the URL fragment fluently.
        /// </summary>
        /// <param name="fragment">The part of the URL afer #</param>
        /// <returns>The Url object with the new fragment set</returns>
        public static IFlurlNavigationManager SetFragment(this IFlurlNavigationManager manager, string fragment)
        {
            manager.Url.SetFragment(fragment);
            return manager;
        }

        /// <summary>
        /// Adds a parameter without a value to the query, removing any existing value.
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <returns>The Url object with the query parameter added</returns>
        public static IFlurlNavigationManager SetQueryParam(this IFlurlNavigationManager manager, string name)
        {
            manager.Url.SetQueryParam(name);
            return manager;
        }

        /// <summary>
        /// Adds a parameter to the query, overwriting the value if name exists.
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <param name="value">Value of query parameter</param>
        /// <param name="isEncoded">Set to true to indicate the value is already URL-encoded</param>
        /// <param name="nullValueHandling">Indicates how to handle null values. Defaults to Remove (any existing)</param>
        /// <returns>The Url object with the query parameter added</returns>
        /// <exception cref="System.ArgumentNullException">Name is null.</exception>
        public static IFlurlNavigationManager SetQueryParam(this IFlurlNavigationManager manager, string name, string value, bool isEncoded = false, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            manager.Url.SetQueryParam(name, value, isEncoded, nullValueHandling);
            return manager;
        }

        /// <summary>
        /// Adds a parameter to the query, overwriting the value if name exists.
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <param name="value">Value of query parameter</param>
        /// <param name="nullValueHandling">Indicates how to handle null values. Defaults to Remove (any existing)</param>
        /// <returns>The Url object with the query parameter added</returns>
        public static IFlurlNavigationManager SetQueryParam(this IFlurlNavigationManager manager, string name, object value, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            manager.Url.SetQueryParam(name, value, nullValueHandling);
            return manager;
        }

        /// <summary>
        /// Adds multiple parameters without values to the query.
        /// </summary>
        /// <param name="names">Names of query parameters.</param>
        /// <returns>The Url object with the query parameter added</returns>
        public static IFlurlNavigationManager SetQueryParams(this IFlurlNavigationManager manager, IEnumerable<string> names)
        {
            manager.Url.SetQueryParams(names);
            return manager;
        }

        /// <summary>
        /// Parses values (usually an anonymous object or dictionary) into name/value pairs
        /// and adds them to the query, overwriting any that already exist.
        /// </summary>
        /// <param name="values">Typically an anonymous object, ie: new { x = 1, y = 2 }</param>
        /// <param name="nullValueHandling">Indicates how to handle null values. Defaults to Remove (any existing)</param>
        /// <returns>The Url object with the query parameters added</returns>
        public static IFlurlNavigationManager SetQueryParams(this IFlurlNavigationManager manager, object values, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            manager.Url.SetQueryParams(values, nullValueHandling);
            return manager;
        }

        /// <summary>
        /// Adds multiple parameters without values to the query.
        /// </summary>
        /// <param name="names">Names of query parameters</param>
        /// <returns>The Url object with the query parameter added.</returns>
        public static IFlurlNavigationManager SetQueryParams(this IFlurlNavigationManager manager, params string[] names)
        {
            manager.Url.SetQueryParams(names);
            return manager;
        }
    }
}