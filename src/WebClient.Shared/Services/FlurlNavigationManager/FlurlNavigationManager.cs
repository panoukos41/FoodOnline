using Flurl;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOnline.WebClient.Services
{
    /// <summary>
    /// todo
    /// </summary>
    public class FlurlNavigationManager : IFlurlNavigationManager
    {
        public Url Url { get; set; }

        public NavigationManager NavigationManager { get; set; }

        public FlurlNavigationManager(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
            Url = new Url(navigationManager.BaseUri);
        }

        public FlurlNavigationManager(NavigationManager navigationManager, Url url)
        {
            NavigationManager = navigationManager;
            Url = url;
        }

        public QueryObjectCollection ParseQueryParams()
        {
            var objs = NavigationManager
                .ToAbsoluteUri(NavigationManager.Uri)
                .Query
                .Remove(0, 1)
                .Split('&');

            var collection = new QueryObjectCollection();

            foreach (string obj in objs)
            {
                var queryItem = obj.Split('=');
                collection[queryItem[0]] = queryItem[1];
            }

            return collection;
        }

        public T ParseQueryObject<T>() where T : new()
        {
            var type = typeof(T);
            T obj = (T)Activator.CreateInstance(type);
            var queryParams = ParseQueryParams();
            var properties = type.GetProperties().Where(x => x.CanRead && x.CanWrite);

            foreach (var property in properties)
            {
                if (queryParams.ContainsKey(property.Name))
                {
                    property.SetValue(obj, queryParams[property.Name]);
                }
            }

            return obj;
        }

        public void Navigate(bool forceLoad = false)
        {
            NavigationManager.NavigateTo(Url.ToString());
        }
    }

    public class QueryObjectCollection : List<QueryObject>
    {
        public QueryObjectCollection()
        {
        }

        public string this[string name]
        {
            get => Find(x => x.Name == name).Value;
            set
            {
                if (ContainsKey(name))
                    Find(x => x.Name == name).Value = value;
                else
                    Add(new QueryObject(name, value));
            }
        }

        public bool ContainsKey(string name)
        {
            return Exists(x => x.Name == name);
        }
    }

    public class QueryObject
    {
        public QueryObject()
        {
        }

        public QueryObject(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}