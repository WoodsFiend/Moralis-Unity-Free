namespace MoralisSDK
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "MoralisConfig", menuName = "Scriptable Objects/Moralis Config", order = 1)]
    public class MoralisConfig : ScriptableObject
    {        
        [Tooltip("Don't forget to start the proxy server")]
        public string proxyServerUrl = "http://localhost:4000/proxy";

        public string apiKey;
        private bool useApiKey = false;
        public bool UseApiKey { get => useApiKey; set => useApiKey = value; }
    }
}