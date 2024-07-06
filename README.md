# Description
The unofficial Moralis Unity SDK to easily access web3 data for building web3 games, wallets, marketplaces and more...

# Getting Started
1. [Register for Moralis](https://admin.moralis.io/register) to get an API key
2. Create a MoralisConfig in Resources folder 
(Assets -> Create -> Scriptable Objects -> Moralis Config)
3. Set your MoralisConfig Api Key (for development only, exposes API key in the game client)
4. Open MoralisDemo scene
4. Run the game and view logs

# Proxy Server
## No-Auth Proxy Server
1. Download the latest version of [Moralis-Unity-ProxyServer](https://github.com/WoodsFiend/Moralis-Unity-ProxyServer)
2. Setup and run the proxy server with USE_AUTH = false
3. Disable The Moralis ApiKeyAuthentication component
4. Enable the Moralis ProxyServerAuthentication component
5. Run game and view logs

## Metamask Simple Signed Auth Proxy Server
1. Download the latest version of [Moralis-Unity-ProxyServer](https://github.com/WoodsFiend/Moralis-Unity-ProxyServer)
2. Setup and run the proxy server with USE_AUTH = true
3. Download Metamask from the [Unity Asset Store](https://assetstore.unity.com/packages/decentralization/infrastructure/metamask-246786)
4. Add MetamaskUnity component to the scene
5. Disable the Moralis ApiKeyAuthentication component
6. Enable the Moralis ProxyServerAuthentication component
7. Disable the Moralis ProxyServerAuthentication.InitWithoutAuth
8. Add METAMASK scripting define symbol to Edit -> ProjectSettings -> Player: OtherSettings - Scripting Define Symbols
9. Run game, login with metamask mobile wallet, view logs

# Docs
All API calls are the same as Node.js in the [Official Moralis Documentation](https://docs.moralis.io/web3-data-api/evm/api-reference)