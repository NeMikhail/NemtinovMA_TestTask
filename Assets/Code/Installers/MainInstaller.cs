using UnityEngine;
using Zenject;
using Core;
using Weather;
using Web;
using DogFacts;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCore();
        BindWebModule();
        BindWeatherModule();
        BindDogsModule();
        BindLoadingModule();
    }

    private void BindCore()
    {
        Container.Bind<Presenters>().AsSingle();
        Container.Bind<GameFactory>().AsSingle();
    }

    private void BindWebModule()
    {
        Container.Bind<WebRequestsQueue>().AsSingle();
        Container.Bind<WebRequestsManager>().AsSingle();
        Container.Bind<APIRequester>().AsSingle();
    }

    private void BindWeatherModule()
    {
        Container.Bind<WeatherEventBus>().AsSingle().NonLazy();
        Container.Bind<WeatherModelsContainer>().AsSingle();
        Container.Bind<WeatherPresenter>().AsSingle();
        Container.Bind<WeatherUIPresenter>().AsSingle();
    }

    private void BindDogsModule()
    {
        Container.Bind<DogFactsEventBus>().AsSingle().NonLazy();
        Container.Bind<FactsModelsContainer>().AsSingle();
        Container.Bind<DogFactsPresenter>().AsSingle();
        Container.Bind<DogFactsUIPresenter>().AsSingle();
    }

    private void BindLoadingModule()
    {
        Container.Bind<LoadingPresenter>().AsSingle();
    }
}
