using CarparkAvailabilityCheckingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarparkAvailabilityCheckingSystem.Services
{
    public class CarparkService : ICarparkService
    {

        private const string API_URL = "https://api.data.gov.sg/v1/transport/carpark-availability";


        public async Task<CarparkModel> GetCarparkAvailability()
        {
            CarparkModel CarParkInfo = new CarparkModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage Res = await client.GetAsync("");
                
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var CarResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Carpark Model
                    CarParkInfo = JsonConvert.DeserializeObject<CarparkModel>(CarResponse);

                }

                return CarParkInfo;
            }
        }

    }
}
