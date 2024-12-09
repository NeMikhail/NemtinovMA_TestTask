using Core.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using Zenject;

namespace Web
{
    public class WebRequestsManager : IPreInitialisation, IFixedExecute, ICleanUp
    {
        private WebRequestsQueue _webRequestsQueue;
        private bool _isBusy;
        private UnityWebRequest _currentRequest;

        [Inject]
        public void Construct(WebRequestsQueue webRequestsQueue)
        {
            _webRequestsQueue = webRequestsQueue;
        }

        public void PreInitialisation()
        {
            _isBusy = false;
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            if (!_isBusy & !_webRequestsQueue.IsEmpty())
            {
                ProcessRequest().Forget();
            }
        }

        public void Cleanup()
        {
            if (_currentRequest != null)
            {
                _currentRequest.Abort();
            }
            _webRequestsQueue.Clear();
        }

        private async UniTaskVoid ProcessRequest()
        {
            UnityWebRequest webRequest = _webRequestsQueue.GetRequest();
            _currentRequest = webRequest;
            await webRequest.SendWebRequest();
            _currentRequest = null;
            _isBusy = false;
        }
    }
}

