using UnityEngine;
using Weather;

public class SceneView : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private WeatherPanelView _weatherView;

    public GameObject LoadingScreen { get => _loadingScreen; }
    public WeatherPanelView WeatherView { get => _weatherView; }
    
}
