using System;
using UnityEngine;
using Zenject;
using Weather;
using Web;

namespace Core
{
    public class GameFactory
    {
        private Presenters _presenters;
        private DiContainer _di;

        [Inject]
        public void Construct(DiContainer di, Presenters presenters)
        {
            _di = di;
            _presenters = presenters;
        }

        public GameFactory()
        {
            
        }

        public GameFactory(Presenters presenters)
        {
            _presenters = presenters;
            Init();
        }

        public void Init()
        {
            InitializeWebModule();
            InitializeScene();
            InitializeWeatherModule();
            InitializeLoadingScreenModule();
        }

        private void InitializeWebModule()
        {
            WebRequestsManager webQueueManager = _di.Resolve<WebRequestsManager>();
            _presenters.Add(webQueueManager);
        }

        private void InitializeScene()
        {
            GameObject canvas =
                _di.InstantiatePrefab(_di.Resolve<PrefabsContainer>().ScenePrefab);
            SceneView sceneView = canvas.GetComponent<SceneView>();
            _di.Bind<SceneView>().FromInstance(sceneView).AsSingle();
        }

        private void InitializeWeatherModule()
        {
            WeatherPresenter weatherPresenter = _di.Resolve<WeatherPresenter>();
            _presenters.Add(weatherPresenter);
            WeatherUIPresenter weatherUIPresenter = _di.Resolve<WeatherUIPresenter>();
            _presenters.Add(weatherUIPresenter);
        }

        private void InitializeLoadingScreenModule()
        {
            LoadingPresenter loadingPresenter = _di.Resolve<LoadingPresenter>();
            _presenters.Add(loadingPresenter);
        }
    }
}