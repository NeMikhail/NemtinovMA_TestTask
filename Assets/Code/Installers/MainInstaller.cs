using UnityEngine;
using Zenject;
using Core;
using Weather;
using Web;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Presenters>().AsSingle();
        Container.Bind<GameFactory>().AsSingle();

        Container.Bind<WebRequestsQueue>().AsSingle();
        Container.Bind<WebRequestsManager>().AsSingle();
        Container.Bind<APIRequester>().AsSingle();

        Container.Bind<WeatherEventBus>().AsSingle().NonLazy();
        Container.Bind<WeatherModelsContainer>().AsSingle();
        Container.Bind<WeatherPresenter>().AsSingle();
        Container.Bind<WeatherUIPresenter>().AsSingle();

        Container.Bind<LoadingPresenter>().AsSingle();
    }
}
