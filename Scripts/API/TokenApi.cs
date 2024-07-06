namespace MoralisSDK
{
    using UnityEngine;
    using UnityEngine.Networking;
    using Newtonsoft.Json;
    using MoralisObjects;
    using Cysharp.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;

    public class TokenApi
    {
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-wallet-token-balances
        public async UniTask<ERC20Token[]> getWalletTokenBalances(string walletAddress, EvmChain chain, string[] tokenAddresses = null, bool excludeSpam = false, string toBlock = null)
        {
            string path = $"{walletAddress}/erc20?chain={chain}";

            if (tokenAddresses != null && tokenAddresses.Length > 0)
            {
                for (int i = 0; i < tokenAddresses.Length; i++)
                {
                    path += $"&token_addresses[{i}]={tokenAddresses[i]}";
                }
            }

            if (excludeSpam)
            {
                path += $"&exclude_spam=true";
            }
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
                ERC20Token[] data = JsonConvert.DeserializeObject<ERC20Token[]>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-token-allowance
        public async UniTask<string> getTokenAllowance(string address, string ownerAddress, string spenderAddress, EvmChain chain)
        {
            string path = $"erc20/{address}/allowance?chain={chain}&owner_address={ownerAddress}&spender_address={spenderAddress}";

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
                Dictionary<string, string> responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

                // Extract the value of the "address" field
                string allowance;
                if (responseData.TryGetValue("allowance", out allowance))
                {
                    return allowance;
                }
                else
                {
                    Debug.LogError("Allowance not found in response.");
                    return null;
                }
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-wallet-token-transfers
        public async UniTask<ERC20WalletTransfers> getWalletTokenTransfers(string walletAddress, EvmChain chain, string[] tokenAddresses = null, string cursor = null, Order order = Order.DESC, string fromBlock = null, string toBlock = null, string fromDate = null, string toDate = null, int limit = 100)
        {
            string path = $"{walletAddress}/erc20/transfers?chain={chain}&order={order}&limit={limit}";

            if (tokenAddresses != null && tokenAddresses.Length > 0)
            {
                for (int i = 0; i < tokenAddresses.Length; i++)
                {
                    path += $"&token_addresses[{i}]={tokenAddresses[i]}";
                }
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
                ERC20WalletTransfers data = JsonConvert.DeserializeObject<ERC20WalletTransfers>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-token-transfers
        public async UniTask<ERC20WalletTransfers> getTokenTransfers(string address, EvmChain chain, string cursor = null, Order order = Order.DESC, string fromBlock = null, string toBlock = null, string fromDate = null, string toDate = null, int limit = 100)
        {
            string path = $"erc20/{address}/transfers?chain={chain}&order={order}&limit={limit}";

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
                ERC20WalletTransfers data = JsonConvert.DeserializeObject<ERC20WalletTransfers>(jsonResponse);
                return data;
            }
        }


        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-token-metadata-by-symbol
        public async UniTask<TokenData[]> getTokenMetadataBySymbol(string[] symbols, EvmChain chain)
        {
            string path = $"erc20/metadata/symbols?chain={chain}";
            if (symbols != null && symbols.Length > 0)
            {
                for (int i = 0; i < symbols.Length; i++)
                {
                    path += $"&symbols[{i}]={symbols[i]}";
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
                TokenData[] data = JsonConvert.DeserializeObject<TokenData[]>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-token-metadata
        public async UniTask<TokenData[]> getTokenMetadata(string[] addresses, EvmChain chain)
        {
            string path = $"erc20/metadata?chain={chain}";
            if (addresses != null && addresses.Length > 0)
            {
                for (int i = 0; i < addresses.Length; i++)
                {
                    path += $"&addresses[{i}]={addresses[i]}";
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
                TokenData[] data = JsonConvert.DeserializeObject<TokenData[]>(jsonResponse);
                return data;
            }
        }

        //Prices endpoints
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/price/get-multiple-token-prices
        public async UniTask<TokenPrice[]> getMultipleTokenPrices(TokenRequest[] tokens, EvmChain chain, bool includePercentChange = false)
        {
            string path = $"erc20/prices?chain={chain}";
            if (includePercentChange)
            {
                path += "&include=percent_change";
            }
            TokenPrice[] data = new TokenPrice[0];

            //We have to batch this in groups of 25 or less
            var chunkArr = Tools.ArrayChunker.ChunkArray(tokens, 25);
            foreach (var chunk in chunkArr) {
                var postData = $@"
                {{
                    ""tokens"": {JsonConvert.SerializeObject(chunk)}
                }}";
                UnityWebRequest request = await Moralis.EvmApi.SendPostRequest(path, postData);

                // Check for errors
                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(request.error);
                    return null;
                }
                else
                {
                    // Deserialize JSON response
                    string jsonResponse = request.downloadHandler.text;
                    data = data.Concat(JsonConvert.DeserializeObject<TokenPrice[]>(jsonResponse)).ToArray();
                }
            }
            return data;
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/price/get-token-price
        public async UniTask<TokenPrice> getTokenPrice(string tokenAddress, EvmChain chain, EvmExchange exchange = EvmExchange.ANY, bool includePercentChange = false)
        {
            string path = $"erc20/{tokenAddress}/price?chain={chain}";
            if(exchange != EvmExchange.ANY)
            {
                path += $"&exchange={exchange}";
            }
            if (includePercentChange)
            {
                path += "&include=percent_change";
            }
            UnityWebRequest request = await Moralis.EvmApi.SendGetRequest(path);

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                return null;
            }
            else
            {
                // Deserialize JSON response
                string jsonResponse = request.downloadHandler.text;
                TokenPrice data = JsonConvert.DeserializeObject<TokenPrice>(jsonResponse);
                return data;
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-token-stats
        public async UniTask<ERC20TokenStats> getTokenStats(string tokenAddress, EvmChain chain)
        {
            string path = $"erc20/{tokenAddress}/stats?chain={chain}";

            UnityWebRequest request = await Moralis.EvmApi.SendGetRequest(path);

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                return null;
            }
            else
            {
                // Deserialize JSON response
                string jsonResponse = request.downloadHandler.text;
                ERC20TokenStats data = JsonConvert.DeserializeObject<ERC20TokenStats>(jsonResponse);
                return data;
            }
        }
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-token-owners
        public async UniTask<TokenOwnersInfo> getTokenOwners(string address, EvmChain chain, string cursor = null, Order order = Order.DESC, int limit = 100)
        {
            string path = $"erc20/{address}/owners?chain={chain}&order={order}&limit={limit}";

            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                TokenOwnersInfo data = JsonConvert.DeserializeObject<TokenOwnersInfo>(jsonResponse);
                return data;
            }
        }
    }
}