using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GutesWetter
{
    public class WetterAPIService
    {
        public static async Task ErmittleWetter(WetterInfo info)
        {
            try
            {
                HttpClient client = new HttpClient();
                string json = await client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={info.Name}&units=metric&appid=84d84c7b399d88e7f4e4688facc2498e");
                info.IsLoaded = true;
                WetterApiResult result = JsonConvert.DeserializeObject<WetterApiResult>(json);

                info.Temperature = Math.Round(result.main.temp,2);
                info.IconUrl = $"http://openweathermap.org/img/w/{result.weather[0].icon}.png";
                info.IsLoaded = true;
            }
            catch (Exception exp)
            {

            }
        }
    }
}
