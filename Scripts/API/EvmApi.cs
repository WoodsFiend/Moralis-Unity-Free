namespace MoralisSDK
{
    using Cysharp.Threading.Tasks;
    using MoralisSDK.MoralisObjects;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    public class EvmApi
    {
        private string authenticationToken;
        private MoralisConfig config;
        private const string EVM_URL = "https://deep-index.moralis.io/api/v2.2";
        public NftApi nft;
        public TokenApi token;
        public DefiApi defi;
        public BalanceApi balance;
        public BlockchainApi block;
        public EventsApi events;
        public TransactionApi transaction;
        public WalletApi wallets;
        public DomainApi resolve;

        public EvmApi(MoralisConfig config, string authenticationToken)
        {
            this.nft = new NftApi();
            this.token = new TokenApi();
            this.defi = new DefiApi();
            this.balance = new BalanceApi();
            this.block = new BlockchainApi();
            this.events = new EventsApi();
            this.transaction = new TransactionApi();
            this.wallets = new WalletApi();
            this.resolve = new DomainApi();

            this.authenticationToken = authenticationToken;
            this.config = config;
        }

        //Send GET request with API key and authentication
        public async UniTask<UnityWebRequest> SendGetRequest(string path)
        {
            string uri = "";
            UnityWebRequest request = null;
            if (config.UseApiKey)
            {
                uri = Path.Combine(EVM_URL, path);
                request = UnityWebRequest.Get(uri);
                request.SetRequestHeader("accept", "application/json");
                request.SetRequestHeader("X-Api-Key", authenticationToken);
            }
            else
            {
                uri = Path.Combine(config.proxyServerUrl, path);
                request = UnityWebRequest.Get(uri);
                request.SetRequestHeader("accept", "application/json");
                request.SetRequestHeader("api", "evm");
                request.SetRequestHeader("Authorization", authenticationToken);
            }

            // Create TaskCompletionSource to track when the web request is done
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            // Send the web request
            var operation = request.SendWebRequest();

            // Set up a callback to handle the request completion
            operation.completed += asyncOperation =>
            {
                // The web request is done, complete the TaskCompletionSource
                tcs.SetResult(true);
            };

            // Wait until the web request is done
            await tcs.Task;
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                var error = JsonConvert.DeserializeObject<RequestError>(request.downloadHandler.text);
                if (error != null && !string.IsNullOrEmpty(error.Message))
                {
                    Debug.LogError(request.error + ": " + error.Message);
                }
                else
                {
                    Debug.LogError(request.error);
                }
            }
            return request;
        }

        //Send POST request with API key and authentication
        public async Task<UnityWebRequest> SendPostRequest(string path, string data)
        {
            string uri = "";
            UnityWebRequest request = null;
            if (config.UseApiKey)
            {
                uri = Path.Combine(EVM_URL, path);
                request = UnityWebRequest.Post(uri, data, "application/json");
                request.SetRequestHeader("accept", "application/json");
                request.SetRequestHeader("X-Api-Key", authenticationToken);
            }
            else
            {
                uri = Path.Combine(config.proxyServerUrl, path);
                request = UnityWebRequest.Post(uri, data, "application/json");
                request.SetRequestHeader("accept", "application/json");
                request.SetRequestHeader("api", "evm");
                request.SetRequestHeader("Authorization", authenticationToken);
            }

            // Create TaskCompletionSource to track when the web request is done
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            // Send the web request
            var operation = request.SendWebRequest();

            // Set up a callback to handle the request completion
            operation.completed += asyncOperation =>
            {
                // The web request is done, complete the TaskCompletionSource
                tcs.SetResult(true);
            };

            // Wait until the web request is done
            await tcs.Task;
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                var error = JsonConvert.DeserializeObject<RequestError>(request.downloadHandler.text);
                if (error != null && !string.IsNullOrEmpty(error.Message))
                {
                    Debug.LogError(request.error + ": " + error.Message);
                }
                else
                {
                    Debug.LogError(request.error);
                }
            }
            return request;
        }

        //Send PUT request with API key and authentication
        public async Task<UnityWebRequest> SendPutRequest(string path, string data)
        {
            string uri = "";
            UnityWebRequest request = null;
            if (config.UseApiKey)
            {
                uri = Path.Combine(EVM_URL, path);
                request = UnityWebRequest.Put(uri, data);
                request.SetRequestHeader("accept", "application/json");
                request.SetRequestHeader("X-Api-Key", authenticationToken);
            }
            else
            {
                uri = Path.Combine(config.proxyServerUrl, path);
                request = UnityWebRequest.Put(uri, data);
                request.SetRequestHeader("accept", "application/json");
                request.SetRequestHeader("api", "evm");
                request.SetRequestHeader("Authorization", authenticationToken);
            }

            // Create TaskCompletionSource to track when the web request is done
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            // Send the web request
            var operation = request.SendWebRequest();

            // Set up a callback to handle the request completion
            operation.completed += asyncOperation =>
            {
                // The web request is done, complete the TaskCompletionSource
                tcs.SetResult(true);
            };

            // Wait until the web request is done
            await tcs.Task;
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                var error = JsonConvert.DeserializeObject<RequestError>(request.downloadHandler.text);
                if (error != null && !string.IsNullOrEmpty(error.Message))
                {
                    Debug.LogError(request.error + ": " + error.Message);
                }
                else
                {
                    Debug.LogError(request.error);
                }
            }
            return request;
        }

    }
}