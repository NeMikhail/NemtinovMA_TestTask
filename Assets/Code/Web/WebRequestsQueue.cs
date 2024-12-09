using System.Collections.Generic;
using UnityEngine.Networking;
using Zenject;

namespace Web
{
    public class WebRequestsQueue
    {
        private Queue<UnityWebRequest> _requestsQueue;

        [Inject]
        public void Construct()
        {
            _requestsQueue = new Queue<UnityWebRequest>();
        }

        public void AddRequest(UnityWebRequest request)
        {
            _requestsQueue.Enqueue(request);
        }

        public UnityWebRequest GetRequest()
        {
            UnityWebRequest webRequest = _requestsQueue.Dequeue();
            return webRequest;
        }

        public bool IsEmpty()
        {
            bool isEmpty = true;
            if (_requestsQueue.Count != 0)
            {
                isEmpty = false;
            }
            return isEmpty;
        }

        public void Clear()
        {
            _requestsQueue.Clear();
        }
    }
}

