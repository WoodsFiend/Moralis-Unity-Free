namespace MoralisSDK
{
    using System.IO;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;
    using MoralisObjects;
    using Cysharp.Threading.Tasks;

    public class NftApi
    {
        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-wallet-nfts
        public async UniTask<NFTBalance> getWalletNFTs(string walletAddress, EvmChain chain, string[] tokenAddresses = null, bool excludeSpam = false, string cursor = null, string format = "decimal", int limit = 100)
        {
            string path = $"{walletAddress}/nft?chain={chain}&format={format}&limit={limit}";

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

            if (excludeSpam)
            {
                path += "&exclude_spam=true";
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
                NFTBalance data = JsonConvert.DeserializeObject<NFTBalance>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-metadata
        public async UniTask<NFT> getNFTMetadata(string address, string tokenId, EvmChain chain)
        {
            var path = Path.Combine("nft", address, tokenId + "?format=decimal&media_items=false&chain=" + chain);
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
                NFT data = JsonConvert.DeserializeObject<NFT>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-contract-nfts
        public async UniTask<NFTBalance> getContractNFTs(string address, EvmChain chain, int totalRanges = 0, int range = 0, string cursor = "", int limit = 100)
        {
            string path = $"nft/{address}?chain={chain}&format=decimal&limit={limit}";
            if (totalRanges > 0)
            {
                path += $"&totalRanges={totalRanges}";
            }
            if (range > 0)
            {
                path += $"&range={range}";
            }
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                NFTBalance data = JsonConvert.DeserializeObject<NFTBalance>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-multiple-nfts
        public async UniTask<NFT[]> getMultipleNFTs(TokenRequest[] tokens, EvmChain chain)
        {
            string path = $"nft/getMultipleNFTs?chain={chain}";
            var postData = $@"
            {{
                ""tokens"": {JsonConvert.SerializeObject(tokens)},
                ""normalizeMetadata"": false,
                ""media_items"": false
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
                NFT[] data = JsonConvert.DeserializeObject<NFT[]>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/resync-metadata
        public async UniTask<bool> reSyncMetadata(string address, string tokenId, EvmChain chain, ResyncType flag = ResyncType.uri, ResyncMode mode = ResyncMode.async)
        {
            string path = $"nft/{address}/{tokenId}/metadata/resync?chain={chain}&flag={flag}&mode={mode}";

            UnityWebRequest request = await Moralis.EvmApi.SendGetRequest(path);
            Debug.Log(request.url);
            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                return false;
            }
            else
            {
                // Return success
                return true;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-wallet-nft-transfers
        public async UniTask<NFTTransferData> getWalletNFTTransfers(string walletAddress, EvmChain chain, Order order = Order.DESC, string[] contractAddresses = null, string cursor = "", string fromBlock = "", string toBlock = "", string fromDate = "", string toDate = "", int limit = 100)
        {
            string path = $"{walletAddress}/nft/transfers?chain={chain}&format=decimal&order={order}&limit={limit}";

            //Append contract addresses if provided
            if (contractAddresses != null && contractAddresses.Length > 0)
            {
                for (int i = 0; i < contractAddresses.Length; i++)
                {
                    path += $"&contract_addresses[{i}]={contractAddresses[i]}";
                }
            }
            //Append optional params if provided
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
                NFTTransferData data = JsonConvert.DeserializeObject<NFTTransferData>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-contract-transfers
        public async UniTask<NFTTransferData> getNFTContractTransfers(string address, EvmChain chain, Order order = Order.DESC, string cursor = "", string fromBlock = "", string toBlock = "", string fromDate = "", string toDate = "", int limit = 100)
        {
            string path = $"nft/{address}/transfers?chain={chain}&format=decimal&order={order}&limit={limit}";

            //Append optional params if provided
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
                NFTTransferData data = JsonConvert.DeserializeObject<NFTTransferData>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-transfers-from-to-block
        public async UniTask<NFTTransferData> getNFTTransfersFromToBlock(EvmChain chain, string fromBlock = "", string toBlock = "", string fromDate = "", string toDate = "", Order order = Order.DESC, string cursor = "", int limit = 100)
        {
            string path = $"nft/transfers?chain={chain}&format=decimal&order={order}&limit={limit}";

            if(string.IsNullOrEmpty(fromBlock) && string.IsNullOrEmpty(toBlock) && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                throw new System.Exception("Needs at least one of 'from_block', 'to_block', 'from_date', 'to_date'");
            }
            //Append optional params if provided
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
                NFTTransferData data = JsonConvert.DeserializeObject<NFTTransferData>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-transfers-by-block
        public async UniTask<NFTTransferData> getNFTTransfersByBlock(string blockNumOrHash, EvmChain chain, Order order = Order.DESC, string cursor = "", int limit = 100)
        {
            string path = $"block/{blockNumOrHash}/nft/transfers?chain={chain}&order={order}&limit={limit}";

            //Append optional params if provided
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                NFTTransferData data = JsonConvert.DeserializeObject<NFTTransferData>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-transfers
        public async UniTask<NFTTransferData> getNFTTransfers(string address, string tokenId, EvmChain chain, Order order = Order.DESC, string cursor = "", int limit = 100)
        {
            string path = $"nft/{address}/{tokenId}/transfers?format=decimal&chain={chain}&order={order}&limit={limit}";

            //Append optional params if provided
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                NFTTransferData data = JsonConvert.DeserializeObject<NFTTransferData>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-wallet-nft-collections
        public async UniTask<NFTWalletCollections> getWalletNFTCollections(string address, EvmChain chain, string cursor = "", bool tokenCounts = false, bool excludeSpam = false, int limit = 100)
        {
            string path = $"{address}/nft/collections?chain={chain}&limit={limit}";

            //Append optional params if provided
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
            }
            if (tokenCounts)
            {
                path += "&token_counts=true";
            }
            if (excludeSpam)
            {
                path += "&exclude_spam=true";
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
                NFTWalletCollections data = JsonConvert.DeserializeObject<NFTWalletCollections>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-metadata-for-nft-contract
        public async UniTask<NFTContractMetadata> getNFTContractMetadata(string address, EvmChain chain)
        {
            string path = $"nft/{address}/metadata?chain={chain}";
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
                NFTContractMetadata data = JsonConvert.DeserializeObject<NFTContractMetadata>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/sync-nft-contract
        public async UniTask<bool> syncNFTContract(string address, EvmChain chain)
        {
            string path = $"nft/{address}/sync?chain={chain}";
            UnityWebRequest request = await Moralis.EvmApi.SendPutRequest(path, "");
            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                return false;
            }
            else
            {
                //No errors, return success
                return true;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-owners
        public async UniTask<NFTWalletCollections> getNFTOwners(string address, EvmChain chain, string cursor = "", int limit = 100)
        {
            string path = $"nft/{address}/owners?chain={chain}&format=decimal&limit={limit}";
            //Append optional params if provided
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                NFTWalletCollections data = JsonConvert.DeserializeObject<NFTWalletCollections>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-token-id-owners
        public async UniTask<NFTWalletCollections> getNFTTokenIdOwners(string address, string tokenId, EvmChain chain, string cursor = "", int limit = 100)
        {
            string path = $"nft/{address}/{tokenId}/owners?chain={chain}&format=decimal&limit={limit}";
            //Append optional params if provided
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                NFTWalletCollections data = JsonConvert.DeserializeObject<NFTWalletCollections>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-trades
        public async UniTask<NFTTrades> getNFTTrades(string address, EvmChain chain, string cursor = "", string fromBlock = "", string toBlock = "", string fromDate = "", string toDate = "", int limit = 100)
        {
            string path = $"nft/{address}/trades?marketplace=opensea&chain={chain}&limit={limit}";
            //Append optional params if provided
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
            if (!string.IsNullOrEmpty(cursor))
            {
                path += $"&cursor={cursor}";
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
                NFTTrades data = JsonConvert.DeserializeObject<NFTTrades>(jsonResponse);
                return data;
            }
        }


        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-lowest-price
        public async UniTask<NFTTrade> getNFTLowestPrice(string address, EvmChain chain, int days = 30)
        {
            string path = $"nft/{address}/lowestprice?marketplace=opensea&chain={chain}";
            if (days > 0)
            {
                path += $"&days={days}";
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
                NFTTrade data = JsonConvert.DeserializeObject<NFTTrade>(jsonResponse);
                return data;
            }
        }
        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-collection-stats
        public async UniTask<OwnershipTransferData> getNFTCollectionStats(string address, EvmChain chain)
        {
            string path = $"nft/{address}/stats?chain={chain}";
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
                OwnershipTransferData data = JsonConvert.DeserializeObject<OwnershipTransferData>(jsonResponse);
                return data;
            }
        }

        //Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/get-nft-token-stats
        public async UniTask<OwnershipTransferData> getNFTTokenStats(string address, string tokenId, EvmChain chain)
        {
            string path = $"nft/{address}/{tokenId}/stats?chain={chain}";
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
                OwnershipTransferData data = JsonConvert.DeserializeObject<OwnershipTransferData>(jsonResponse);
                return data;
            }
        }
    }
}