using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BPH_ER_Smart_Kiosk.Models;
using Newtonsoft.Json;
using BPH_ER_Smart_Kiosk.Helpers;


namespace ThaiSmartCardReader.Services
{
    internal class APIService
    {
        private readonly HttpClient _httpClient;

        public APIService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<(NHSOPersonDataModel? Data, string ErrorMessage)> GetNHSODataSmartCardAsync()
        {
            string url = "http://127.0.0.1:8189/api/smartcard/read?readImageFlag=false";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return (JsonConvert.DeserializeObject<NHSOPersonDataModel>(json), "SUCCESS");
                }
                else
                {
                    Logger.Log("Error Read SmartCard: " + json);
                    string errorMessage = response.StatusCode switch
                    {
                        System.Net.HttpStatusCode.InternalServerError => "ERR_NOT_FOUND_CARD",
                        (System.Net.HttpStatusCode)418 => "ERR_NOT_FOUND_SMARTCARD_READER",
                        _ => $"ERR_HTTP"
                    };

                    return (null, errorMessage);
                }
            }
            catch (TimeoutException ex)
            {
                Debug.WriteLine($"Timeout occurred: {ex.Message}");
                Logger.Log("Timeout occurred: " + ex.Message);
                return (null, "ERR_TIMEOUT");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
                Logger.Log("Exception occurred: " + ex.Message);
                return (null, "ERR_EXCEPTION");
            }
        }
        private class ResponseConfirmSaveModel
        {
            [JsonProperty("correlationId")]
            public string? CorrelationId { get; set; }
            [JsonProperty("hn")]
            public string? Hn { get; set; }
            [JsonProperty("hcode")]
            public string? Hcode { get; set; }
            [JsonProperty("claimType")]
            public string? ClaimType { get; set; }
            [JsonProperty("mobile")]
            public string? Mobile { get; set; }
            [JsonProperty("pid")]
            public string? Pid { get; set; }
            [JsonProperty("claimCode")]
            public string? ClaimCode { get; set; }
        }
    }
}
