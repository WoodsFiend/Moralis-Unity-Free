namespace MoralisSDK
{
    using Cysharp.Threading.Tasks;
    using MoralisSDK.MoralisObjects;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;

    public class DefiApi
    {
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-pair-reserves
        public async UniTask<PairReserve> getPairReserves(string pairAddress, EvmChain chain, string toBlock = null, string toDate = null)
        {
            string path = $"{pairAddress}/reserves?chain={chain}";

            if (!string.IsNullOrEmpty(toBlock))
            {
                path += $"&to_block={toBlock}";
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                path += $"&to_date={toDate}";
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
                PairReserve data = JsonConvert.DeserializeObject<PairReserve>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-pair-address
        public async UniTask<PairInfo> getPairAddress(string token0Address, string token1Address, EvmChain chain, EvmExchange exchange, string toBlock = null, string toDate = null)
        {
            string path = $"{token0Address}/{token1Address}/pairAddress?chain={chain}&exchange={exchange}";

            if (!string.IsNullOrEmpty(toBlock))
            {
                path += $"&to_block={toBlock}";
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                path += $"&to_date={toDate}";
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
                PairInfo data = JsonConvert.DeserializeObject<PairInfo>(jsonResponse);
                return data;
            }
        }
    }
}