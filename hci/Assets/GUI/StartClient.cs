using System;
using UnityEngine;

namespace ControllerEmulation
{
    public class StartClient : MonoBehaviour
    {
        private Client _client;
        public ControllerInputData _input;

        private void Start()
        {
            _input = new ControllerInputData();
            _client = new Client(_input);
            
        }
        
        private void OnApplicationQuit()
        {
            _client?.StopClient();
            Debug.Log("Client stopped.");
        }

        public void StartClientNow()
        {
            _client.StartClient();
        }

        public void StopClient()
        {
            _client.StopClient();
        }
    }
}