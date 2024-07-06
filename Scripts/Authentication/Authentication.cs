namespace MoralisSDK.Authentication
{
    using MoralisSDK;
    using UnityEngine;

    public abstract class Authentication : MonoBehaviour
    {
        protected void Authenticate(string authString)
        {
            Moralis.Init(authString);
        }
    }
}