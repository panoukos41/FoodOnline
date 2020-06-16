using Flurl;
using Microsoft.AspNetCore.Components;

namespace FoodOnline.WebClient.Services
{
    public interface IFlurlNavigationManager
    {
        /// <summary>
        /// Gets or sets the NavigationManager to use for navigation.
        /// </summary>
        NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Gets or sets the URL to be called.
        /// </summary>
        Url Url { get; set; }

        /// <summary>
        /// Navigates to the Url that was build.
        /// </summary>
        /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new
        /// page from the server, whether or not the URI would normally be handled by the
        /// client-side router.</param>
        public void Navigate(bool forceLoad = false);
    }
}