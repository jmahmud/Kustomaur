using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Kustomaur.Models;

namespace Kustomaur.Azure
{
    public class Portal
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://management.azure.com";

        private const string API_VERSION = "2019-01-01-preview";
        private const string DASHBOARD_URL =
            "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Portal/dashboards/{dashboardName}";
        
        public Portal(IHttpClientFactory httpClientFactory, string baseUrl = "")
        {
            _httpClientFactory = httpClientFactory;
            if (!string.IsNullOrEmpty(baseUrl))
            {
                _baseUrl = baseUrl;
            }

            _httpClient = _httpClientFactory.CreateClient();
        }

        public async Task CreateOrUpdateDashboard(Models.Dashboard dashboard, string subscriptionId, string resourceGroupName, string dashboardName)
        {
            var url = FormatDashboardUrl(subscriptionId, resourceGroupName, dashboardName);
            
            var dashboardStr = Dashboard.Generator.Generate(dashboard.Properties);



            var content = new StringContent(dashboardStr, Encoding.UTF8, "application/json");
            var httpClientRequest = new HttpRequestMessage();
            httpClientRequest.Content = content;
            httpClientRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            

            var result = await _httpClient.PutAsync(url, content);
            if (!result.IsSuccessStatusCode)
            {
                var error = await result.Content.ReadAsStringAsync();
            }
        }

        private string FormatDashboardUrl(string subscriptionId, string resourceGroupName, string dashboardName)
        {
            var formattedRoute = DASHBOARD_URL.Replace($"{{{nameof(subscriptionId)}}}", subscriptionId);
            formattedRoute = formattedRoute.Replace($"{{{nameof(resourceGroupName)}}}", resourceGroupName);
            formattedRoute = formattedRoute.Replace($"{{{nameof(dashboardName)}}}", dashboardName);
            return _baseUrl + formattedRoute + $"?api-version={API_VERSION}";
        }
        
    }
}