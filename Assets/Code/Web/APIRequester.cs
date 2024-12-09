using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Extention;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using Weather;
using Zenject;

namespace Web
{
    public class APIRequester
    {
        private WebRequestsQueue _webRequestsQueue;

        [Inject]
        public void Construct(WebRequestsQueue webRequestsQueue)
        {
            _webRequestsQueue = webRequestsQueue;
        }

        public async UniTask<List<WeatherModel>> GetWeatherData(string apiURL)
        {
            UnityWebRequest request = UnityWebRequest.Get(apiURL);
            _webRequestsQueue.AddRequest(request);
            await UniTask.WaitUntil(() => request.isDone || request.isNetworkError);
            string results = request.downloadHandler.text;
            request.Dispose();
            dynamic json = JObject.Parse(results);
            dynamic properties = json["properties"];
            dynamic periods = properties["periods"];
            List<WeatherModel> weatherModels = new List<WeatherModel>();
            foreach (dynamic period in periods)
            {
                WeatherModel model = new WeatherModel();
                model.Name = (string)period["name"];
                model.Temperature = (string)period["temperature"];
                model.TemperatureUnit = (string)period["temperatureUnit"];
                model.WindSpeed = (string)period["windSpeed"];
                model.WindDirection = (string)period["windDirection"];
                model.IconUrl = (string)period["icon"];
                model.ShortForecast = (string)period["shortForecast"];
                model.DetailedForecast = (string)period["detailedForecast"];
                model.IsDaytime = (bool)period["isDaytime"];
                weatherModels.Add(model);
            }

            return weatherModels;
        }

        public void SaveData(dynamic json)
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Weather.json");
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            File.WriteAllText(path, json.ToString());
        }
    }
}

