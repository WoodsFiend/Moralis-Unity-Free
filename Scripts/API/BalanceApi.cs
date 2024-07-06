namespace MoralisSDK
{
    using Cysharp.Threading.Tasks;
    using MoralisSDK.MoralisObjects;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;

    public class BalanceApi
    {
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-native-balance
        public async UniTask<NativeToken> getNativeBalance(string walletAddress, EvmChain chain, string toBlock = null)
        {
            string path = $"{walletAddress}/balance?chain={chain}";

            if (!string.IsNullOrEmpty(toBlock))
            {
                path += $"&to_block={toBlock}";
            }

            UnityWebRequest request = await Moralis.EvmApi.SendGetRequest(path);

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                return null;
            }
            else
            {
                // Deserialize JSON response
                string jsonResponse = request.downloadHandler.text;
                NativeToken data = JsonConvert.DeserializeObject<NativeToken>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-native-balances-for-addresses
        public async UniTask<ChainBalanceData[]> getNativeBalancesForAddresses(string[] walletAddresses, EvmChain chain, string toBlock = null)
        {
            string path = $"wallets/balances?chain={chain}";

            if (!string.IsNullOrEmpty(toBlock))
            {
                path += $"&to_block={toBlock}";
            }
            if (walletAddresses != null && walletAddresses.Length > 0)
            {
                for (int i = 0; i < walletAddresses.Length; i++)
                {
                    path += $"&wallet_addresses[{i}]={walletAddresses[i]}";
                }
            }
            UnityWebRequest request = await Moralis.EvmApi.SendGetRequest(path);

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                return null;
            }
            else
            {
                // Deserialize JSON response
                string jsonResponse = request.downloadHandler.text;
                ChainBalanceData[] data = JsonConvert.DeserializeObject<ChainBalanceData[]>(jsonResponse);
                return data;
            }
        }
    }
}
