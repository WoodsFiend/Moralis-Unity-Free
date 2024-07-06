namespace MoralisSDK
{
    using Cysharp.Threading.Tasks;
    using MoralisSDK.MoralisObjects;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;

    public class TransactionApi
    {
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-transaction
        public async UniTask<Transaction> getTransaction(string transactionHash, EvmChain chain, bool includeTransactions = false)
        {
            string path = $"transaction/{transactionHash}?chain={chain}";
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
                Transaction data = JsonConvert.DeserializeObject<Transaction>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-decoded-transaction
        public async UniTask<Transaction> getTransactionVerbose(string transactionHash, EvmChain chain, bool includeTransactions = false)
        {
            string path = $"transaction/{transactionHash}/verbose?chain={chain}";
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
                Transaction data = JsonConvert.DeserializeObject<Transaction>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-wallet-transactions
        public async UniTask<NativeWalletTransactions> getWalletTransactions(string address, EvmChain chain, bool includeTransactions = false, Order order = Order.DESC, string cursor = null, string fromBlock = null, string toBlock = null, string fromDate = null, string toDate = null, int limit = 100)
        {
            string path = $"{address}?chain={chain}&limit={limit}&order={order}";
            if (includeTransactions)
            {
                path += $"&include=internal_transactions";
            }
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                NativeWalletTransactions data = JsonConvert.DeserializeObject<NativeWalletTransactions>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-decoded-wallet-transaction
        public async UniTask<NativeWalletTransactions> getWalletTransactionsVerbose(string address, EvmChain chain, bool includeTransactions = false, Order order = Order.DESC, string cursor = null, string fromBlock = null, string toBlock = null, string fromDate = null, string toDate = null, int limit = 100)
        {
            string path = $"{address}/verbose?chain={chain}&limit={limit}&order={order}";
            if (includeTransactions)
            {
                path += $"&include=internal_transactions";
            }
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                NativeWalletTransactions data = JsonConvert.DeserializeObject<NativeWalletTransactions>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-internal-transactions
        public async UniTask<InternalTransaction[]> getInternalTransactions(string transactionHash, EvmChain chain)
        {
            string path = $"transaction/{transactionHash}/internal-transactions?chain={chain}";

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
                InternalTransaction[] data = JsonConvert.DeserializeObject<InternalTransaction[]>(jsonResponse);
                return data;
            }
        }

    }
}