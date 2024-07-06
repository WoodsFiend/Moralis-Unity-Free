namespace MoralisSDK.Authentication.Metamask
{
    using UnityEngine;
    using System;
    using UnityEngine.Events;
    using Newtonsoft.Json;

    //Metamask Unity must be installed https://assetstore.unity.com/packages/decentralization/infrastructure/metamask-246786
    //METAMASK scripting define must be added to Project Settings -> Player -> Other -> Scripting Define Symbols
#if METAMASK
    using MetaMask.Unity;
    using Cysharp.Threading.Tasks;
#endif

    // Prompts a user to connect and sign a message with their metamask mobile wallet
    // Fires an Authenticated event with a string in a format that is used by the default proxy server
    public class MetaMaskProxyServerLogin : MonoBehaviour
    {
        public bool initOnStart = true;
        public string signMessage = "This is a test message";

        public UnityEvent<string> Authenticated = new UnityEvent<string>();
#if METAMASK
        private string signedMessage;

        private async void Start()
        {
            if (initOnStart)
            {
                signedMessage = await ConnectAndSign();
            }
        }

        public async UniTask<string> ConnectAndSign()
        {
            if (MetaMaskUnity.Instance != null)
            {
                var wallet = MetaMaskUnity.Instance.Wallet;
                wallet.WalletAuthorized += OnWalletAuthorized;
                wallet.WalletConnected += OnWalletConnected;
                wallet.AccountChanged += OnAccountChanged;
                var signed = await wallet.ConnectAndSign(signMessage);
                //Sometimes it connects before signing, sometimes it doesn't
                if (!string.IsNullOrEmpty(MetaMaskUnity.Instance.Wallet.SelectedAddress))
                {
                    var auth = new WalletAuth(MetaMaskUnity.Instance.Wallet.SelectedAddress, signed);
                    Authenticated.Invoke(JsonConvert.SerializeObject(auth));
                }
                return signed;
            }
            else
            {
                return null;
            }
        }
        private void OnDestroy()
        {
            if (MetaMaskUnity.Instance != null)
            {
                var wallet = MetaMaskUnity.Instance.Wallet;
                wallet.WalletAuthorized -= OnWalletAuthorized;
                wallet.WalletConnected -= OnWalletConnected;
                wallet.AccountChanged -= OnAccountChanged;
            }
        }

        private void OnWalletConnected(object sender, EventArgs e)
        {
            Debug.Log("Wallet is connected");
        }

        private void OnWalletAuthorized(object sender, EventArgs e)
        {
            Debug.Log("Wallet is authorized");
        }
        private void OnAccountChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(signedMessage))
            {
                var auth = new WalletAuth(MetaMaskUnity.Instance.Wallet.SelectedAddress, signedMessage);
                Authenticated.Invoke(JsonConvert.SerializeObject(auth));
            }
        }
#endif
    }

    //Data structure for default proxy server to parse and handle
    public class WalletAuth
    {
        public string walletAddress;
        public string signedMessage;

        public WalletAuth(string walletAddress, string signedMessage)
        {
            this.walletAddress = walletAddress;
            this.signedMessage = signedMessage;
        }
    }
}