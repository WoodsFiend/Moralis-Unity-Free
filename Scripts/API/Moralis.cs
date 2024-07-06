namespace MoralisSDK
{
    using UnityEngine;
    //The base class for the Moralis SDK
    //Can easily be extended with more API functionality
    public class Moralis
    {
        public static EvmApi EvmApi;
        public static bool isInitialized = false;

        private static MoralisConfig config;

        public static void Init(string authenticationToken)
        {
            if (!isInitialized)
            {
                config = Resources.Load<MoralisConfig>("MoralisConfig");
                if (config == null)
                {
                    Debug.LogError("Moralis.Init: MoralisConfig not found in Resources Folder");
                    return;
                }
                EvmApi = new EvmApi(config, authenticationToken);
                isInitialized = true;
            }
        }
    }
}