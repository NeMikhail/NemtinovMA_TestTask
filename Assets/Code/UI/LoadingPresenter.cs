using Core.Interface;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using Weather;

public class LoadingPresenter : IInitialisation, ICleanUp
{
    private SceneView _sceneView;
    private WeatherEventBus _weatherEventBus;

    private bool _isWeatherContentLoaded;
    private bool _isDogsContentLoaded;


    [Inject]
    public void Construct(SceneView sceneView, WeatherEventBus weatherEventBus)
    {
        _sceneView = sceneView;
        _weatherEventBus = weatherEventBus;
    }

    public void Initialisation()
    {
        SetDogsLoaded();
        _weatherEventBus.OnSceneContentInitialized += SetWeatherLoaded;
    }
    public void Cleanup()
    {
        _weatherEventBus.OnSceneContentInitialized -= SetWeatherLoaded;
    }

    private void SetWeatherLoaded()
    {
        _isWeatherContentLoaded = true;
        CheckLoadingConditions();
    }

    private void SetDogsLoaded()
    {
        _isDogsContentLoaded = true;
        CheckLoadingConditions();
    }

    private void CheckLoadingConditions()
    {
        if (_isWeatherContentLoaded & _isDogsContentLoaded)
        {
            _sceneView.LoadingScreen.SetActive(false);
        }
    }
}
