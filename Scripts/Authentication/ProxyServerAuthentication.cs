using UnityEngine;

namespace MoralisSDK.Authentication
{
    // Initializes Moralis SDK to use the Proxy Server with a string for the Authorization Header
    // This could be any authentication string as long as your proxy server handles it correcly to authorize the user for proxy requests
    public class ProxyServerAuthentication : Authentication
    {
        [SerializeField]
        private bool initWithoutAuth = true;
        private void Start() 
        {
            if (initWithoutAuth)
            {
                Authenticate("");
            }
        }
        public void OnAuthenticated(string authToken)
        {
            Authenticate(authToken);
        }
    }
}