namespace MoralisSDK
{
    using Cysharp.Threading.Tasks;
    using MoralisSDK.MoralisObjects;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;

    public class WalletApi
    {
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/wallet-api/get-wallet-history
        public async UniTask<TransactionData> getWalletHistory(string address, EvmChain chain, string cursor = null, bool includeInternalTransactions = false, bool includeInputData = false, bool includeNftMetadata = false, Order order = Order.DESC, string fromBlock = null, string toBlock = null, string fromDate = null, string toDate = null, int limit = 100)
        {
            string path = $"wallets/{address}/history?chain={chain}&limit={limit}&order={order}";
            if (includeInternalTransactions)
            {
                path += $"&include_internal_transactions=true";
            }
            if (includeInputData)
            {
                path += $"&include_input_data=true";
            }
            if (includeNftMetadata)
            {
                path += $"&nft_metadata=true";
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
                TransactionData data = JsonConvert.DeserializeObject<TransactionData>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/wallet-api/get-wallet-net-worth
        public async UniTask<NetWorthData> getWalletNetWorth(string address, EvmChain[] chains = null, bool excludeSpam = true, bool excludeUnverifiedContracts = true)
        {
            string path = $"wallets/{address}/net-worth?";
            if (excludeSpam)
            {
                path += $"&exclude_spam=true";
            }
            if (excludeUnverifiedContracts)
            {
                path += $"&exclude_unverified_contracts=true";
            }

            if (chains != null && chains.Length > 0)
            {
                for (int i = 0; i < chains.Length; i++)
                {
                    path += $"&chains[{i}]={chains[i]}";
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
                NetWorthData data = JsonConvert.DeserializeObject<NetWorthData>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/wallet-api/get-wallet-token-balances-price
        public async UniTask<TokenBalancesPrice> getWalletTokenBalancesPrice(string address, EvmChain chain, string[] tokenAddresses = null, string cursor = null, bool excludeSpam = true, bool excludeUnverifiedContracts = true, bool excludeNative = true, string toBlock = null, int limit = 100)
        {
            string path = $"wallets/{address}/tokens?chain={chain}&limit={limit}";
            if (excludeSpam)
            {
                path += $"&exclude_spam=true";
            }
            if (excludeUnverifiedContracts)
            {
                path += $"&exclude_unverified_contracts=true";
            }
            if (excludeNative)
            {
                path += $"&exclude_native=true";
            }
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
            }
            if (!string.IsNullOrEmpty(toBlock))
            {
                path += $"&to_block={toBlock}";
            }
            if (tokenAddresses != null && tokenAddresses.Length > 0)
            {
                for (int i = 0; i < tokenAddresses.Length; i++)
                {
                    path += $"&token_addresses[{i}]={tokenAddresses[i]}";
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
                TokenBalancesPrice data = JsonConvert.DeserializeObject<TokenBalancesPrice>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/wallet-api/get-chain-activity-by-wallet
        public async UniTask<WalletActiveChains> getWalletActiveChains(string address, EvmChain[] chains = null)
        {
            string path = $"wallets/{address}/chains?";

            if (chains != null && chains.Length > 0)
            {
                for (int i = 0; i < chains.Length; i++)
                {
                    path += $"&chains[{i}]={chains[i]}";
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
                WalletActiveChains data = JsonConvert.DeserializeObject<WalletActiveChains>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-native-balances-for-addresses
        public async UniTask<BlockStats> getWalletStats(string address, EvmChain chain)
        {
            string path = $"wallets/{address}/stats?chain={chain}";

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