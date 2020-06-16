using Flurl;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace FoodOnline.WebClient.Services
{
    public static class NavigationManagerExtensions
    {
        //todo deserialize query parames to object

        /// <summary>
        /// Appends a segment to the URL path, ensuring there is one and only one '/' character as a seperator.
        /// </summary>
        /// <param name="segment">The segment to append</param>
        /// <param name="fullyEncode">If true, URL-encodes reserved characters such as '/', '+', and '%'. Otherwise,
        /// only encodes strictly illegal characters (including '%' but only when not followed
        /// by 2 hex characters).</param>
        /// <returns>the Url object with the segment appended</returns>
        /// <exception cref="System.ArgumentNullException">Segment is null.</exception>
        public static IFlurlNavigationManager AppendPathSegment(this NavigationManager manager, object segment, bool fullyEncode = false)
        {
            return new FlurlNavigationManager(manager).AppendPathSegment(segment, fullyEncode);
        }

        /// <summary>
        /// Appends multiple segments to the URL path, ensuring there is one and only one
        /// '/' character as a seperator.
        /// </summary>
        /// <param name="segments">The segments to append</param>
        /// <returns>The Url object with the segments appended</returns>
        public static IFlurlNavigationManager AppendPathSegments(this NavigationManager manager, params object[] segments)
        {
            return new FlurlNavigationManager(manager).AppendPathSegments(segments);
        }

        /// <summary>
        /// Appends multiple segments to the URL path, ensuring there is one and only one
        /// '/' character as a seperator.
        /// </summary>
        /// <param name="segments">The segments to append</param>
        /// <returns>The Url object with the segments appended</returns>
        public static IFlurlNavigationManager AppendPathSegments(this NavigationManager manager, IEnumerable<object> segments)
        {
            return new FlurlNavigationManager(manager).AppendPathSegments(segments);
        }

        /// <summary>
        /// Removes the URL fragment including the #.
        /// </summary>
        /// <returns>The Url object with the fragment removed</returns>
        public static IFlurlNavigationManager RemoveFragment(this NavigationManager manager)
        {
            return new FlurlNavigationManager(manager).RemoveFragment();
        }

        /// <summary>
        /// Removes a name/value pair from the query by name.
        /// </summary>
        /// <param name="name">Query string parameter name to remove</param>
        /// <returns>The Url object with the query parameter removed</returns>
        public static IFlurlNavigationManager RemoveQueryParam(this NavigationManager manager, string name)
        {
            return new FlurlNavigationManager(manager).RemoveQueryParam(name);
        }

        /// <summary>
        /// Removes multiple name/value pairs from the query by name.
        /// </summary>
        /// <param name="names">Query string parameter names to remove</param>
        /// <returns>The Url object with the query parameters removed</returns>
        public static IFlurlNavigationManager RemoveQueryParams(this NavigationManager manager, IEnumerable<string> names)
        {
            return new FlurlNavigationManager(manager).RemoveQueryParams(names);
        }

        /// <summary>
        /// Removes multiple name/value pairs from the query by name.
        /// </summary>
        /// <param name="names">Query string parameter names to remove</param>
        /// <returns>The Url object with the query parameters removed</returns>
        public static IFlurlNavigationManager RemoveQueryParams(this NavigationManager manager, params string[] names)
        {
            return new FlurlNavigationManager(manager).RemoveQueryParams(names);
        }

        /// <summary>
        /// Resets the URL to its root, including the scheme, any user info, host, and port
        /// (if specified).
        /// </summary>
        /// <returns>The Url object trimmed to its root.</returns>
        public static IFlurlNavigationManager ResetToRoot(this NavigationManager manager)
        {
            return new FlurlNavigationManager(manager).ResetToRoot();
        }

        /// <summary>
        /// Set the URL fragment fluently.
        /// </summary>
        /// <param name="fragment">The part of the URL afer #</param>
        /// <returns>The Url object with the new fragment set</returns>
        public static IFlurlNavigationManager SetFragment(this NavigationManager manager, string fragment)
        {
            return new FlurlNavigationManager(manager).SetFragment(fragment);
        }

        /// <summary>
        /// Adds a parameter without a value to the query, removing any existing value.
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <returns>The Url object with the query parameter added</returns>
        public static IFlurlNavigationManager SetQueryParam(this NavigationManager manager, string name)
        {
            return new FlurlNavigationManager(manager).SetQueryParam(name);
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
        public static IFlurlNavigationManager SetQueryParam(this NavigationManager manager, string name, string value, bool isEncoded = false, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            return new FlurlNavigationManager(manager).SetQueryParam(name, value, isEncoded, nullValueHandling);
        }

        /// <summary>
        /// Adds a parameter to the query, overwriting the value if name exists.
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <param name="value">Value of query parameter</param>
        /// <param name="nullValueHandling">Indicates how to handle null values. Defaults to Remove (any existing)</param>
        /// <returns>The Url object with the query parameter added</returns>
        public static IFlurlNavigationManager SetQueryParam(this NavigationManager manager, string name, object value, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            return new FlurlNavigationManager(manager).SetQueryParam(name, value, nullValueHandling);
        }

        /// <summary>
        /// Adds multiple parameters without values to the query.
        /// </summary>
        /// <param name="names">Names of query parameters.</param>
        /// <returns>The Url object with the query parameter added</returns>
        public static IFlurlNavigationManager SetQueryParams(this NavigationManager manager, IEnumerable<string> names)
        {
            return new FlurlNavigationManager(manager).SetQueryParams(names);
        }

        /// <summary>
        /// Parses values (usually an anonymous object or dictionary) into name/value pairs
        /// and adds them to the query, overwriting any that already exist.
        /// </summary>
        /// <param name="values">Typically an anonymous object, ie: new { x = 1, y = 2 }</param>
        /// <param name="nullValueHandling">Indicates how to handle null values. Defaults to Remove (any existing)</param>
        /// <returns>The Url object with the query parameters added</returns>
        public static IFlurlNavigationManager SetQueryParams(this NavigationManager manager, object values, NullValueHandling nullValueHandling = NullValueHandling.Remove)
        {
            return new FlurlNavigationManager(manager).SetQueryParams(values, nullValueHandling);
        }

        /// <summary>
        /// Adds multiple parameters without values to the query.
        /// </summary>
        /// <param name="names">Names of query parameters</param>
        /// <returns>The Url object with the query parameter added.</returns>
        public static IFlurlNavigationManager SetQueryParams(this NavigationManager manager, params string[] names)
        {
            return new FlurlNavigationManager(manager).SetQueryParams(names);
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static QueryObjectCollection ParseQueryParams(this NavigationManager manager)
        {
            return new FlurlNavigationManager(manager).ParseQueryParams();
        }
    }
}