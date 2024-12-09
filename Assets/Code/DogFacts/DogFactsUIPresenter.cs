using Core.Interface;
using System;
using UnityEngine;
using Zenject;
using Extention;

namespace DogFacts
{
    public class DogFactsUIPresenter : IInitialisation, ICleanUp
    {
        private const float POPUP_STRING_LENGTH = 34f;
        private const float POPUP_STRING_HEIGHT = 40f;
        private const float POPUP_BASIC_HEIGHT = 200f;
        private DiContainer _di;
        private PrefabsContainer _prefabsContainer;
        private FactsModelsContainer _factsModels;
        private DogFactsEventBus _dogFactsEventBus;
        private FactsPanelView _dogsPanelView;
        private SerializableDictionary<FactButtonView, DogFactModel> _factButtonsDict;


        [Inject]
        public void Construct(DiContainer diContainer, PrefabsContainer prefabsContainer,
            FactsModelsContainer factsModels, DogFactsEventBus dogFactsEventBus,
            SceneView sceneView)
        {
            _di = diContainer;
            _prefabsContainer = prefabsContainer;
            _factsModels = factsModels;
            _dogFactsEventBus = dogFactsEventBus;
            _dogsPanelView = sceneView.FactsView;
        }

        public void Initialisation()
        {
            _dogFactsEventBus.OnDogModelsLoaded += CreateUI;
            _factButtonsDict = new SerializableDictionary<FactButtonView, DogFactModel>();
            _dogsPanelView.Initialize();
        }


        public void Cleanup()
        {
            _dogFactsEventBus.OnDogModelsLoaded -= CreateUI;
            foreach (FactButtonView factButtonView in _dogsPanelView.FactButtonViews)
            {
                factButtonView.Button.onClick.RemoveAllListeners();
            }
        }

        private void CreateUI()
        {
            int index = 1;
            foreach (DogFactModel model in _factsModels.DogFactModels)
            {
                GameObject dogButtonObject =
                    _di.InstantiatePrefab(_prefabsContainer.DogButtonPrefab, _dogsPanelView.FactsScrollRect);
                FactButtonView factView = dogButtonObject.GetComponent<FactButtonView>();
                SetButtonParametrs(model, factView, index);
                _dogsPanelView.FactButtonViews.Add(factView);
                _factButtonsDict.Add(factView, model);
                index++;
            }
        }

        private void SetButtonParametrs(DogFactModel model, FactButtonView factView, int index)
        {
            factView.FactNameText.text = model.Name;
            factView.NumberText.text = index.ToString();
            factView.Button.onClick.AddListener(delegate { ShowDogPopUp(factView); });
        }

        private void ShowDogPopUp(FactButtonView factButtonView)
        {
            FactPopupView popubView = _dogsPanelView.FactPopupView;
            DogFactModel model = _factButtonsDict.GetValue(factButtonView);
            int stringsCount = (int)(model.Description.Length / POPUP_STRING_LENGTH);
            popubView.PopupPanelRect.sizeDelta = 
                new Vector2(popubView.PopupPanelRect.sizeDelta.x,
                POPUP_BASIC_HEIGHT + (stringsCount * POPUP_STRING_HEIGHT));
            popubView.NameText.text = model.Name;
            popubView.DescriptionText.text = model.Description;
            popubView.gameObject.SetActive(true);
        }
    }
}
