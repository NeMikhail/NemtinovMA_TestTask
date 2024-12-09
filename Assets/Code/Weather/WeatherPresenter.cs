using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Core.Interface;
using Zenject;
using System;
using UniRx;
using UnityEngine;
using Web;

namespace Weather
{
    public class WeatherPresenter : IInitialisation, ICleanUp
    {
        private const string WEATHER_API_URL = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        private const float TIMEER_DELAY = 5f;
        private WeatherEventBus _weatherEventBus;
        private WeatherModelsContainer _weatherModels;
        private bool _isFirstRequest;
        private APIRequester _api;
        private CompositeDisposable _disposables;


        [Inject]
        public void Construct(WeatherEventBus weatherEventBus, WeatherModelsContainer weatherModels, 
            APIRequester api)

        {
            _api = api;
            _weatherEventBus = weatherEventBus;
            _weatherModels = weatherModels;
        }

        public void Initialisation()
        {
            
            _disposables = new CompositeDisposable();
            _isFirstRequest = true;
            AsyncInit().Forget();
            
        }
        public void Cleanup()
        {
            if (_disposables != null)
            {
                _disposables.Dispose();
            }
        }

        private async UniTaskVoid AsyncInit()
        {
            await GetWeatherInfo();
            Observable.Timer(System.TimeSpan.FromSeconds(TIMEER_DELAY))
                .Repeat()
                .Subscribe(_ => { GetWeatherInfo(); }).AddTo(_disposables);
        }

        private async UniTask GetWeatherInfo()
        {
            await RequestWeather();
            //Debug.Log("Weather request ended");
        }

        private async UniTask RequestWeather()
        {
            List<WeatherModel> weatherModels = new List<WeatherModel>();
            UniTask<List<WeatherModel>> requestTask = _api.GetWeatherData(WEATHER_API_URL);
            weatherModels = await requestTask;
            UpdateModels(weatherModels);
            if (_isFirstRequest)
            {
                _isFirstRequest = false;
                _weatherEventBus.OnFirstInitialization?.Invoke();
            }
            else
            {
                _weatherEventBus.OnModelUpdated?.Invoke();
            }
        }

        private void UpdateModels(List<WeatherModel> weatherModels)
        {
            _weatherModels.ClearModels();
            foreach (WeatherModel model in weatherModels)
            {
                if (model.IsDaytime)
                {
                    _weatherModels.DayWeatherModels.Add(model);
                }
                else
                {
                    _weatherModels.NightWeatherModels.Add(model);
                }
            }
        }

    }
}
