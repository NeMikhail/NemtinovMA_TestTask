using Core.Interface;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Web;
using Zenject;

namespace DogFacts
{
    public class DogFactsPresenter : IInitialisation
    {
        private const string DOGS_API_URL = "https://dogapi.dog/api/v2/breeds";
        private APIRequester _api;
        private FactsModelsContainer _factsModels;
        private DogFactsEventBus _dogFactsEventBus;


        [Inject]
        public void Construct(FactsModelsContainer factsModels,
        APIRequester api, DogFactsEventBus dogFactsEventBus)
        {
            _api = api;
            _factsModels = factsModels;
            _dogFactsEventBus = dogFactsEventBus;
        }

        public void Initialisation()
        {
            GetDogFacts().Forget();
        }

        private async UniTask GetDogFacts()
        {
            List<DogFactModel> dogFactsList = new List<DogFactModel>();
            UniTask<List<DogFactModel>> requestTask = _api.GetDogsData(DOGS_API_URL);
            dogFactsList = await requestTask;
            foreach (DogFactModel model in dogFactsList)
            {
                _factsModels.DogFactModels.Add(model);
            }
            _dogFactsEventBus.OnDogModelsLoaded?.Invoke();
        }

    }
}
