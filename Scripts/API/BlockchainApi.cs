namespace MoralisSDK
{
    using Cysharp.Threading.Tasks;
    using MoralisSDK.MoralisObjects;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;

    public class BlockchainApi
    {
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-block
        public async UniTask<BlockData> getBlock(string blockNumOrHash, EvmChain chain, bool includeTransactions = false)
        {
            string path = $"block/{blockNumOrHash}?chain={chain}";
            if (includeTransactions)
            {
                path += $"&include=internal_transactions";
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
                BlockData data = JsonConvert.DeserializeObject<BlockData>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-date-to-block
        public async UniTask<BlockInfo> getDateToBlock(string unixDate, EvmChain chain)
        {
            string path = $"dateToBlock?chain={chain}&date={unixDate}";

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
                BlockInfo data = JsonConvert.DeserializeObject<BlockInfo>(jsonResponse);
                return data;
            }
        }
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-block-stats
        public async UniTask<BlockStats> getBlockStats(string blockNumOrHash, EvmChain chain)
        {
            string path = $"block/{blockNumOrHash}/stats?chain={chain}";

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
                BlockStats data = JsonConvert.DeserializeObject<BlockStats>(jsonResponse);
                return data;
            }
        }
    }
}