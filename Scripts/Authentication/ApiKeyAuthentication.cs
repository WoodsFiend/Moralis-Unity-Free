namespace MoralisSDK.Authentication
{
    using UnityEngine;

    // Initializes Moralis SDK to use the API directly

    // Development purposes only
    // This is not advised for production as the api key is exposed
    // Should use ProxyServerAuthentication with the proxy server
    public class ApiKeyAuthentication : Authentication
    {
        MoralisConfig config;

        // Start is called before the first frame update
        void Start()
        {
            config = Resources.Load<MoralisConfig>("MoralisConfig");
            if (config == null)
            {
                Debug.LogError("MoralisConfig not found in Resources folder");
                return;
            }
            if (string.IsNullOrEmpty(config.apiKey))
            {
                Debug.LogError("MoralisConfig apiKey is not set");
                return;
            }
            config.UseApiKey = true;
            Authenticate(config.apiKey);
        }
    }
}