using System.Collections.Generic;
using UnityEngine;

namespace DogFacts
{
    public class FactsPanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform _factsScrollRect;
        [SerializeField] private FactPopupView _factPopupView;
        private List<FactButtonView> _factButtonViews;

        public RectTransform FactsScrollRect { get => _factsScrollRect; }
        public FactPopupView FactPopupView { get => _factPopupView; }
        public List<FactButtonView> FactButtonViews { get => _factButtonViews; set => _factButtonViews = value; }
        

        public void Initialize()
        {
            _factButtonViews = new List<FactButtonView>();
        }
    }
}

