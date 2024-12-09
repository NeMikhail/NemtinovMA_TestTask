using DogFacts;
using UnityEngine;
using Weather;

public class SceneView : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private WeatherPanelView _weatherView;
    [SerializeField] private FactsPanelView _factsView;

    public GameObject LoadingScreen { get => _loadingScreen; }
    public WeatherPanelView WeatherView { get => _weatherView; }
    public FactsPanelView FactsView { get => _factsView; }
}
