using Newtonsoft.Json;
using System.Text;

namespace Appointment_Scheduler.Client.Services
{
    public static class RequestHandler
    {
    
        public static async  Task<(bool,string)> HandlePostRequest(HttpClient _client, string url, object obj)
        {
            var details = JsonConvert.SerializeObject(obj);
            StringContent stringContent = new StringContent(details, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, stringContent);
            var tempContent = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, tempContent);
        }

        public static async Task<(bool, string)> HandleGetRequest(HttpClient _client, string url)
        {
            
            var response = await _client.GetAsync(url);
            var tempContent = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, tempContent);
        }

        public static async Task<(bool, string)> HandleDeleteRequest(HttpClient _client, string url)
        {

            var response = await _client.DeleteAsync(url);
            var tempContent = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, tempContent);
        }

        public static async Task<(bool, string)> HandlePutRequest(HttpClient _client, string url, object obj)
        {
            var details = JsonConvert.SerializeObject(obj);
            StringContent stringContent = new StringContent(details, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, stringContent);
            var tempContent = await response.Content.ReadAsStringAsync();
            return (response.IsSuccessStatusCode, tempContent);
        }
    }
}
