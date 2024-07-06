namespace MoralisSDK
{
    using Cysharp.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;

    public class DomainApi
    {
        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/wallet-api/resolve-address
        public async UniTask<string> resolveENSAddress(string address)
        {
            string path = $"resolve/{address}/reverse";

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
                string name;
                if (responseData.TryGetValue("name", out name))
                {
                    return name;
                }
                else
                {
                    Debug.LogError("Name not found in response.");
                    return null;
                }
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/wallet-api/resolve-ens-domain
        public async UniTask<string> resolveENSDomain(string domainName)
        {
            string path = $"resolve/ens/{domainName}";

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
                string address;
                if (responseData.TryGetValue("address", out address))
                {
                    return address;
                }
                else
                {
                    Debug.LogError("Address not found in response.");
                    return null;
                }
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/wallet-api/resolve-address-to-domain
        public async UniTask<string> resolveUnstoppableAddress(string address)
        {
            string path = $"resolve/{address}/domain";

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
                string name;
                if (responseData.TryGetValue("name", out name))
                {
                    return name;
                }
                else
                {
                    Debug.LogError("Name not found in response.");
                    return null;
                }
            }
        }

        // Moralis Docs: https://docs.moralis.io/web3-data-api/evm/reference/wallet-api/resolve-domain
        public async UniTask<string> resolveUnstoppableDomain(string domainName, string currency = null)
        {
            string path = $"resolve/{domainName}";
            if (!string.IsNullOrEmpty(currency))
            {
                path += $"?currency={currency}";
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
                Dictionary<string, string> responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

                // Extract the value of the "address" field
                string address;
                if (responseData.TryGetValue("address", out address))
                {
                    return address;
                }
                else
                {
                    Debug.LogError("Address not found in response.");
                    return null;
                }
            }
        }
    }
}