namespace MoralisSDK
{
    using Cysharp.Threading.Tasks;
    using MoralisSDK.MoralisObjects;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;

    public class EventsApi
    {
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-contract-logs
        public async UniTask<ContractLogs> getContractLogs(string address, string topic0, EvmChain chain, Order order = Order.DESC, string cursor = null, string blockNumber = null, string fromBlock = null, string toBlock = null, string fromDate = null, string toDate = null, int limit = 100)
        {
            string path = $"{address}/logs?chain={chain}&order={order}&limit={limit}&topic0={topic0}";
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
            }
            if (!string.IsNullOrEmpty(blockNumber))
            {
                path += $"&block_number={blockNumber}";
            }
            if (!string.IsNullOrEmpty(fromBlock))
            {
                path += $"&from_block={fromBlock}";
            }
            if (!string.IsNullOrEmpty(toBlock))
            {
                path += $"&to_block={toBlock}";
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                path += $"&from_date={fromDate}";
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
                ContractLogs data = JsonConvert.DeserializeObject<ContractLogs>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-contract-events
        public async UniTask<ContractEvents> getContractEvents(string address, string topic, string eventABI, EvmChain chain, Order order = Order.DESC, string offset = null, string cursor = null, string blockNumber = null, string fromBlock = null, string toBlock = null, string fromDate = null, string toDate = null, int limit = 100)
        {
            string path = $"{address}/events?chain={chain}&order={order}&limit={limit}&topic={topic}";
            if (!string.IsNullOrEmpty(offset))
            {
                path += $"&offset={offset}";
            }
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
            }
            if (!string.IsNullOrEmpty(blockNumber))
            {
                path += $"&block_number={blockNumber}";
            }
            if (!string.IsNullOrEmpty(fromBlock))
            {
                path += $"&from_block={fromBlock}";
            }
            if (!string.IsNullOrEmpty(toBlock))
            {
                path += $"&to_block={toBlock}";
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                path += $"&from_date={fromDate}";
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                path += $"&to_date={toDate}";
            }

            UnityWebRequest request = await Moralis.EvmApi.SendPostRequest(path, eventABI);

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
                ContractEvents data = JsonConvert.DeserializeObject<ContractEvents>(jsonResponse);
                return data;
            }
        }
    }
}