using CorporateWebProject.Application.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Utilities.Api
{
    public class ApiTransaction<T> where T : class
    {
        public async Task<T> Get(string url)
        {
            string request = ApiConfiguration.path + url;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, request)))
            {
                try
                {
                    var read = await response.Content.ReadAsStreamAsync();
                    var ss = await response.Content.ReadAsStringAsync();
                    var json = System.Text.Json.JsonSerializer.Deserialize<T>(read);
                    return json;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<T> Get(string url, string data)
        {
            string request = ApiConfiguration.path + url;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, request) { Content = new StringContent(data, Encoding.UTF8, MediaTypeNames.Application.Json) }))
            {
                try
                {
                    var ss = await response.Content.ReadAsStringAsync();
                    var read = await response.Content.ReadAsStreamAsync();
                    var json = System.Text.Json.JsonSerializer.Deserialize<T>(read);
                    return json;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<T> Post(string url)
        {
            string request = ApiConfiguration.path + url;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, request)))
            {
                try
                {
                    var read = await response.Content.ReadAsStreamAsync();
                    var ss = await response.Content.ReadAsStringAsync();
                    var json = System.Text.Json.JsonSerializer.Deserialize<T>(read);
                    return json;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<T> Post(string url, string data)
        {
            string request = ApiConfiguration.path + url;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, request) { Content = new StringContent(data, Encoding.UTF8, MediaTypeNames.Application.Json) }))
            {
                try
                {
                    var ss = await response.Content.ReadAsStringAsync();
                    var read = await response.Content.ReadAsStreamAsync();
                    var json = System.Text.Json.JsonSerializer.Deserialize<T>(read);
                    return json;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<T> Put(string url, string data)
        {
            string request = ApiConfiguration.path + url;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, request) { Content = new StringContent(data, Encoding.UTF8, MediaTypeNames.Application.Json) }))
            {
                try
                {
                    var read = await response.Content.ReadAsStreamAsync();
                    var json = System.Text.Json.JsonSerializer.Deserialize<T>(read);
                    return json;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public async Task<bool> Delete(string url)
        {
            try
            {
                string request = ApiConfiguration.path + url;
                using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, request))) return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
