namespace MoralisSDK
{
    using Newtonsoft.Json;
    using UnityEngine;
    using Cysharp.Threading.Tasks;
    using MoralisSDK.MoralisObjects;
    using System.Threading.Tasks;

    //Demo all Moralis API functions
    public class DemoMoralis : MonoBehaviour
    {
        public bool test;
        public bool runOnStart = true;

        async void Start()
        {
            while (!Moralis.isInitialized)
            {
                await UniTask.Yield();
            }
            Debug.Log("Moralis Demo: Moralis Initialized");

            if (runOnStart)
            {
                //Run test on start
                RunTest();
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Repeat tests
            if (test)
            {
                test = false;
                RunTest();
            }
        }

        private async void RunTest()
        {
            if (Moralis.isInitialized)
            {
                //WARNING: Testing all API endpoints costs 2.8K CUs or 7% of free tier daily allowance
                await TestWalletApi();
                await TestNftApi();
                await TestTokenApi();
                await TestDefiApi();
                await TestBlockApi();
                await TestEventsApi();
                await TestTransactionApi();
            }
        }

        private static async Task TestWalletApi()
        {
            // Get Wallet Transaction History
            var walletHistory = await Moralis.EvmApi.wallets.getWalletHistory("0xd8da6bf26964af9d7eed9e03e53415d37aa96045", EvmChain.ETH);
            if (walletHistory != null)
            {
                Debug.Log("Moralis Demo (Wallets Get Wallet History): " + JsonConvert.SerializeObject(walletHistory));
            }
            // Get wallet net worth
            var netWorth = await Moralis.EvmApi.wallets.getWalletNetWorth("0xE92d1A43df510F82C66382592a047d288f85226f");
            if (netWorth != null)
            {
                Debug.Log("Moralis Demo (Wallets Get Wallet Net Worth): " + JsonConvert.SerializeObject(netWorth));
            }
            // Get native balance for multiple wallets
            var multipleNetWorth = await Moralis.EvmApi.balance.getNativeBalancesForAddresses(new string[] { "0xE92d1A43df510F82C66382592a047d288f85226f" }, EvmChain.ETH);
            if (multipleNetWorth != null)
            {
                Debug.Log("Moralis Demo (Wallets Get Native Balances For Addresses): " + JsonConvert.SerializeObject(multipleNetWorth));
            }
            // Get ERC20 Token Balances with Prices by Wallet
            var erc20TokenBalancesPrice = await Moralis.EvmApi.wallets.getWalletTokenBalancesPrice("0xcB1C1FdE09f811B294172696404e88E658659905", EvmChain.ETH);
            if (erc20TokenBalancesPrice != null)
            {
                Debug.Log("Moralis Demo (Wallets Get ERC20 Balance with Prices): " + JsonConvert.SerializeObject(erc20TokenBalancesPrice));
            }
            // Get chain activity by wallet
            var walletActiveChains = await Moralis.EvmApi.wallets.getWalletActiveChains("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326");
            if (walletActiveChains != null)
            {
                Debug.Log("Moralis Demo (Wallets Get Wallet Active Chains): " + JsonConvert.SerializeObject(walletActiveChains));
            }
            // Get wallet stats
            var walletStats = await Moralis.EvmApi.wallets.getWalletStats("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH);
            if (walletStats != null)
            {
                Debug.Log("Moralis Demo (Wallets Get Wallet Stats): " + JsonConvert.SerializeObject(walletStats));
            }
            // ENS Lookup by Address
            var ensDomain = await Moralis.EvmApi.resolve.resolveENSAddress("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326");
            if (ensDomain != null)
            {
                Debug.Log("Moralis Demo (Wallets Get ENS from Address): " + ensDomain);
            }
            // ENS Lookup By Domain
            var ensAddress = await Moralis.EvmApi.resolve.resolveENSDomain("vitalik.eth");
            if (ensAddress != null)
            {
                Debug.Log("Moralis Demo (Wallets Get Address from ENS): " + ensAddress);
            }
            // Unstoppable Lookup by Address
            var unstoppableDomain = await Moralis.EvmApi.resolve.resolveUnstoppableAddress("0x94ef5300cbc0aa600a821ccbc561b057e456ab23");
            if (unstoppableDomain != null)
            {
                Debug.Log("Moralis Demo (Wallets Get Unstoppable from Address): " + unstoppableDomain);
            }
            // Unstoppable Lookup By Domain
            var unstoppableAddress = await Moralis.EvmApi.resolve.resolveUnstoppableDomain("brad.crypto");
            if (unstoppableAddress != null)
            {
                Debug.Log("Moralis Demo (Wallets Get Address from Unstoppable): " + unstoppableAddress);
            }
        }

        private static async Task TestTransactionApi()
        {
            //Transaction
            // Get transaction by hash
            var transactionDetails = await Moralis.EvmApi.transaction.getTransaction("0xdc85cb1b75fd09c2f6d001fea4aba83764193cbd7881a1fa8ccde350a5681109", EvmChain.ETH);
            if (transactionDetails != null)
            {
                Debug.Log("Moralis Demo (Transaction Get Transaction): " + JsonConvert.SerializeObject(transactionDetails));
            }
            // Get decoded transaction by hash
            var transactionDetailsVerbose = await Moralis.EvmApi.transaction.getTransactionVerbose("0x012b9b98e21664117ec0b499d726a39f492ac8bd402cca8bebcbd163b9f75760", EvmChain.ETH);
            if (transactionDetailsVerbose != null)
            {
                Debug.Log("Moralis Demo (Transaction Get Transaction Verbose): " + JsonConvert.SerializeObject(transactionDetailsVerbose));
            }
            // Get native transactions by wallet
            var nativeWalletTransactions = await Moralis.EvmApi.transaction.getWalletTransactions("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH);
            if (nativeWalletTransactions != null)
            {
                Debug.Log("Moralis Demo (Transaction Get Native Transactions): " + JsonConvert.SerializeObject(nativeWalletTransactions));
            }
            // Get decoded transactions by wallet
            var nativeWalletTransactionsVerbose = await Moralis.EvmApi.transaction.getWalletTransactionsVerbose("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH);
            if (nativeWalletTransactionsVerbose != null)
            {
                Debug.Log("Moralis Demo (Transaction Get Native Transactions Verbose): " + JsonConvert.SerializeObject(nativeWalletTransactionsVerbose));
            }
            // Get internal transactions by transaction hash
            var internalTransactions = await Moralis.EvmApi.transaction.getInternalTransactions("0xdc85cb1b75fd09c2f6d001fea4aba83764193cbd7881a1fa8ccde350a5681109", EvmChain.ETH);
            if (internalTransactions != null)
            {
                Debug.Log("Moralis Demo (Transaction Get Internal Transactions): " + JsonConvert.SerializeObject(internalTransactions));
            }
        }

        private static async Task TestEventsApi()
        {
            //Events
            // Get logs by contract
            var contractLogs = await Moralis.EvmApi.events.getContractLogs("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", "0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef", EvmChain.ETH);
            if (contractLogs != null)
            {
                Debug.Log("Moralis Demo (Events Get Logs by Contract): " + JsonConvert.SerializeObject(contractLogs));
            }
            // Get events by contract
            string eventAbi = @"
                {
                    ""anonymous"": false,
                    ""inputs"": [
                        {
                            ""indexed"": true,
                            ""name"": ""src"",
                            ""type"": ""address""
                        },
                        {
                            ""indexed"": true,
                            ""name"": ""dst"",
                            ""type"": ""address""
                        },
                        {
                            ""indexed"": false,
                            ""name"": ""wad"",
                            ""type"": ""uint256""
                        }
                    ],
                    ""name"": ""Transfer"",
                    ""type"": ""event""
                }";
            var contractEvents = await Moralis.EvmApi.events.getContractEvents("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", "0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef", eventAbi, EvmChain.ETH);
            if (contractEvents != null)
            {
                Debug.Log("Moralis Demo (Events Get Contract Events): " + JsonConvert.SerializeObject(contractEvents));
            }
        }

        private static async Task TestBlockApi()
        {
            //Block
            // Get block by hash
            var blockData = await Moralis.EvmApi.block.getBlock("18541416", EvmChain.ETH);
            if (blockData != null)
            {
                Debug.Log("Moralis Demo (Blockchain Get Block): " + JsonConvert.SerializeObject(blockData));
            }
            // Get block by date
            var dateToBlockData = await Moralis.EvmApi.block.getDateToBlock("1714034638", EvmChain.ETH);
            if (dateToBlockData != null)
            {
                Debug.Log("Moralis Demo (Blockchain Get Date to Block): " + JsonConvert.SerializeObject(dateToBlockData));
            }
            // Get block stats
            var blockStats = await Moralis.EvmApi.block.getBlockStats("18541416", EvmChain.ETH);
            if (blockStats != null)
            {
                Debug.Log("Moralis Demo (Blockchain Get Block Stats): " + JsonConvert.SerializeObject(blockStats));
            }
        }

        private static async Task TestDefiApi()
        {
            //Defi
            // Get DEX token pair reserves
            var pairReserves = await Moralis.EvmApi.defi.getPairReserves("0xa2107fa5b38d9bbd2c461d6edf11b11a50f6b974", EvmChain.ETH);
            if (pairReserves != null)
            {
                Debug.Log("Moralis Demo (ERC20 Get Pair Reserves): " + JsonConvert.SerializeObject(pairReserves));
            }
            // Get DEX token pair address
            var pairInfo = await Moralis.EvmApi.defi.getPairAddress("0x2b591e99afe9f32eaa6214f7b7629768c40eeb39", "0xdac17f958d2ee523a2206206994597c13d831ec7", EvmChain.ETH, EvmExchange.UNISWAPV2);
            if (pairInfo != null)
            {
                Debug.Log("Moralis Demo (ERC20 Get token pair address): " + JsonConvert.SerializeObject(pairInfo));
            }
        }

        private static async Task TestTokenApi()
        {
            //Token
            // Get native token balance of a wallet
            var nativeToken = await Moralis.EvmApi.balance.getNativeBalance("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH);
            if (nativeToken != null)
            {
                Debug.Log("Moralis Demo (Native Token Balance): " + nativeToken.Balance);
            }
            // Get all erc20 token balance of a wallet
            var erc20Balances = await Moralis.EvmApi.token.getWalletTokenBalances("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH);
            if (erc20Balances != null)
            {
                Debug.Log("Moralis Demo (All ERC20 Balances): " + JsonConvert.SerializeObject(erc20Balances));
            }
            // Get a single erc20 token balance of a wallet
            var erc20Balance = await Moralis.EvmApi.token.getWalletTokenBalances("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH, new string[] { "0x57b9d10157f66d8c00a815b5e289a152dedbe7ed" });
            if (erc20Balance != null)
            {
                Debug.Log("Moralis Demo (Single ERC20 Balance): " + JsonConvert.SerializeObject(erc20Balance[0]));
            }
            // Get Multiple ERC20 token prices
            TokenRequest[] tokens = new TokenRequest[]
            {
                new TokenRequest { TokenAddress = "0xdac17f958d2ee523a2206206994597c13d831ec7" },
                new TokenRequest { TokenAddress = "0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48" },
                new TokenRequest { TokenAddress = "0x7d1afa7b718fb893db30a3abc0cfc608aacfebb0" },
                new TokenRequest { TokenAddress = "0xae7ab96520de3a18e5e111b5eaab095312d7fe84", Exchange = EvmExchange.UNISWAPV2, ToBlock = "16314545" }
            };
            var multipleTokenPrices = await Moralis.EvmApi.token.getMultipleTokenPrices(tokens, EvmChain.ETH);
            if (multipleTokenPrices != null)
            {
                Debug.Log("Moralis Demo (Multiple NFTs): " + JsonConvert.SerializeObject(multipleTokenPrices));
            }
            // Get the current price of an erc20 token
            var tokenPrice = await Moralis.EvmApi.token.getTokenPrice("0x514910771AF9Ca656af840dff83E8264EcF986CA", EvmChain.ETH);
            if (tokenPrice != null)
            {
                Debug.Log("Moralis Demo (Token Price): " + JsonConvert.SerializeObject(tokenPrice));
            }
            // Get ERC20 token metadata by symbols
            var symbolTokenMetadata = await Moralis.EvmApi.token.getTokenMetadataBySymbol(new string[] { "LINK" }, EvmChain.ETH);
            if (symbolTokenMetadata != null)
            {
                Debug.Log("Moralis Demo (ERC20 Token Metadata by Symbols): " + JsonConvert.SerializeObject(symbolTokenMetadata));
            }
            // Get ERC20 token metadata by contract
            var tokenMetadata = await Moralis.EvmApi.token.getTokenMetadata(new string[] { "0x514910771AF9Ca656af840dff83E8264EcF986CA" }, EvmChain.ETH);
            if (tokenMetadata != null)
            {
                Debug.Log("Moralis Demo (ERC20 Token Metadata by Contract): " + JsonConvert.SerializeObject(tokenMetadata));
            }
            // Get ERC20 token allowance
            var tokenAllowance = await Moralis.EvmApi.token.getTokenAllowance("0xdac17f958d2ee523a2206206994597c13d831ec7", "0x0cBee687015d5151BA084806E00A59A8e6F206c2", "0x75e89d5979E4f6Fba9F97c104c2F0AFB3F1dcB88", EvmChain.ETH);
            if (tokenAllowance != null)
            {
                Debug.Log("Moralis Demo (ERC20 Token Allowance): " + tokenAllowance);
            }
            // Get ERC20 token transfers by wallet
            var transfersByWallet = await Moralis.EvmApi.token.getWalletTokenTransfers("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH);
            if (transfersByWallet != null)
            {
                Debug.Log("Moralis Demo (ERC20 Get Transfers by Wallet): " + JsonConvert.SerializeObject(transfersByWallet));
            }
            // Get ERC20 token transfers by contract
            var transfersByContract = await Moralis.EvmApi.token.getTokenTransfers("0x7d1afa7b718fb893db30a3abc0cfc608aacfebb0", EvmChain.ETH);
            if (transfersByContract != null)
            {
                Debug.Log("Moralis Demo (ERC20 Get Transfers by Contract): " + JsonConvert.SerializeObject(transfersByContract));
            }
            // Get ERC20 token stats
            var tokenStats = await Moralis.EvmApi.token.getTokenStats("0xdac17f958d2ee523a2206206994597c13d831ec7", EvmChain.ETH);
            if (tokenStats != null)
            {
                Debug.Log("Moralis Demo (ERC20 Get Token Stats): " + JsonConvert.SerializeObject(tokenStats));
            }
            // Get ERC20 token owners
            var tokenOwners = await Moralis.EvmApi.token.getTokenOwners("0x7d1afa7b718fb893db30a3abc0cfc608aacfebb0", EvmChain.ETH);
            if (tokenOwners != null)
            {
                Debug.Log("Moralis Demo (ERC20 Get Token Owners): " + JsonConvert.SerializeObject(tokenOwners));
            }
        }

        private static async Task TestNftApi()
        {
            // Get multiple NFTs
            // Create and initialize an array of Token objects
            TokenRequest[] tokens = new TokenRequest[]
            {
                new TokenRequest { TokenAddress = "0xa4991609c508b6d4fb7156426db0bd49fe298bd8", TokenId = "12" },
                new TokenRequest { TokenAddress = "0x3c64dc415ebb4690d1df2b6216148c8de6dd29f7", TokenId = "1" },
                new TokenRequest { TokenAddress = "0x3c64dc415ebb4690d1df2b6216148c8de6dd29f7", TokenId = "200" }
            };
            var multipleNFTs = await Moralis.EvmApi.nft.getMultipleNFTs(tokens, EvmChain.ETH);
            if (multipleNFTs != null)
            {
                Debug.Log("Moralis Demo (Multiple NFTs): " + JsonConvert.SerializeObject(multipleNFTs));
            }

            // Get all nfts owned by a wallet
            var allNFTs = await Moralis.EvmApi.nft.getWalletNFTs("0xff3879b8a363aed92a6eaba8f61f1a96a9ec3c1e", EvmChain.ETH);
            if (allNFTs != null)
            {
                Debug.Log("Moralis Demo (All Wallet NFTs): " + JsonConvert.SerializeObject(allNFTs.Result));
            }

            // Get all specified nfts owned by a wallet
            var specifiedNFTs = await Moralis.EvmApi.nft.getWalletNFTs("0xff3879b8a363aed92a6eaba8f61f1a96a9ec3c1e", EvmChain.ETH, tokenAddresses: new string[] { "0xfe131caaa9eb3fa774220ee34f617642313847bd" });
            if (specifiedNFTs != null)
            {
                Debug.Log("Moralis Demo (Specified Wallet NFTs): " + JsonConvert.SerializeObject(specifiedNFTs.Result));
            }

            // Get a single nft metadata
            var nft = await Moralis.EvmApi.nft.getNFTMetadata("0xa4991609c508b6d4fb7156426db0bd49fe298bd8", "12", EvmChain.ETH);
            if (nft != null)
            {
                Debug.Log("Moralis Demo (NFT Metadata): " + nft.MetadataSerialized);
            }

            // Get the collection / contract level metadata for a given contract
            var nftContractMetadata = await Moralis.EvmApi.nft.getNFTContractMetadata("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", EvmChain.ETH);
            if (nftContractMetadata != null)
            {
                Debug.Log("Moralis Demo (Collection Metadata): " + JsonConvert.SerializeObject(nftContractMetadata));
            }

            // Get all NFTs from a contract
            var contractNFTs = await Moralis.EvmApi.nft.getContractNFTs("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", EvmChain.ETH);
            if (contractNFTs != null)
            {
                Debug.Log("Moralis Demo (Contract NFTs): " + JsonConvert.SerializeObject(contractNFTs));
            }

            // Resync NFT metadata
            var resyncSuccess = await Moralis.EvmApi.nft.reSyncMetadata("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", "1", EvmChain.ETH);
            if (resyncSuccess)
            {
                Debug.Log("Moralis Demo (ReSync Metadata): Success");
            }
            else
            {
                Debug.Log("Moralis Demo (ReSync Metadata): Failure");
            }
            // Get wallet NFT transfers
            var nftTransfers = await Moralis.EvmApi.nft.getWalletNFTTransfers("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH);
            if (nftTransfers != null)
            {
                Debug.Log("Moralis Demo (NFT Wallet Transfers): " + JsonConvert.SerializeObject(nftTransfers));
            }
            // Get contract NFT transfers
            var nftContractTransfers = await Moralis.EvmApi.nft.getWalletNFTTransfers("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", EvmChain.ETH);
            if (nftContractTransfers != null)
            {
                Debug.Log("Moralis Demo (NFT Contract Transfers): " + JsonConvert.SerializeObject(nftContractTransfers));
            }

            // Get NFT transfers from a block to a block
            var nftBlockToBlockTransfers = await Moralis.EvmApi.nft.getNFTTransfersFromToBlock(EvmChain.ETH, "19942100", "19942174");
            if (nftBlockToBlockTransfers != null)
            {
                Debug.Log("Moralis Demo (NFT Block to Block Transfers): " + JsonConvert.SerializeObject(nftBlockToBlockTransfers));
            }

            // Get NFT transfers from a single block number or hash
            var nftBlockTransfers = await Moralis.EvmApi.nft.getNFTTransfersByBlock("15846571", EvmChain.ETH);
            if (nftBlockTransfers != null)
            {
                Debug.Log("Moralis Demo (NFT Block Transfers): " + JsonConvert.SerializeObject(nftBlockTransfers));
            }
            // Get NFT transfers of a single NFT
            var nftTransfer = await Moralis.EvmApi.nft.getNFTTransfers("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", "1", EvmChain.ETH);
            if (nftTransfer != null)
            {
                Debug.Log("Moralis Demo (NFT Transfers): " + JsonConvert.SerializeObject(nftTransfer));
            }

            // Get NFT collections by wallet
            var nftCollections = await Moralis.EvmApi.nft.getWalletNFTCollections("0x1f9090aaE28b8a3dCeaDf281B0F12828e676c326", EvmChain.ETH);
            if (nftCollections != null)
            {
                Debug.Log("Moralis Demo (NFT Wallet Collections): " + JsonConvert.SerializeObject(nftCollections));
            }

            // Sync NFT Contract
            var syncNFT = await Moralis.EvmApi.nft.syncNFTContract("0x60E4d786628Fea6478F785A6d7e704777c86a7c6", EvmChain.ETH);
            if (syncNFT)
            {
                Debug.Log("Moralis Demo (NFT Sync Contract): Success");
            }
            else
            {
                Debug.Log("Moralis Demo (NFT Sync Contract): Failure");
            }
            // Get NFT owners by contract
            var nftOwners = await Moralis.EvmApi.nft.getNFTOwners("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", EvmChain.ETH);
            if (nftOwners != null)
            {
                Debug.Log("Moralis Demo (NFT Owners): " + JsonConvert.SerializeObject(nftOwners));
            }
            // Get NFT owners by token ID
            var nftOwnersById = await Moralis.EvmApi.nft.getNFTTokenIdOwners("0xb47e3cd837dDF8e4c57F05d70Ab865de6e193BBB", "1", EvmChain.ETH);
            if (nftOwnersById != null)
            {
                Debug.Log("Moralis Demo (NFT Owners By Id): " + JsonConvert.SerializeObject(nftOwnersById));
            }
            // Get NFT trades by marketplace
            var nftTrades = await Moralis.EvmApi.nft.getNFTTrades("0xcc7187ddbe8f099d31bac88d8d67f793001d718e", EvmChain.ETH);
            if (nftTrades != null)
            {
                Debug.Log("Moralis Demo (NFT Trades): " + JsonConvert.SerializeObject(nftTrades));
            }
            // Get the lowest price for an NFT contract
            var nftLowestPrice = await Moralis.EvmApi.nft.getNFTLowestPrice("0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d", EvmChain.ETH);
            if (nftLowestPrice != null)
            {
                Debug.Log("Moralis Demo (NFT Lowest Price): " + JsonConvert.SerializeObject(nftLowestPrice));
            }
            // Get collection stats
            var collectionStats = await Moralis.EvmApi.nft.getNFTCollectionStats("0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb", EvmChain.ETH);
            if (collectionStats != null)
            {
                Debug.Log("Moralis Demo (NFT Collection Stats): " + JsonConvert.SerializeObject(collectionStats));
            }
            // Get NFT token stats
            var nftStats = await Moralis.EvmApi.nft.getNFTTokenStats("0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d", "1", EvmChain.ETH);
            if (nftStats != null)
            {
                Debug.Log("Moralis Demo (NFT Token Stats): " + JsonConvert.SerializeObject(nftStats));
            }
        }
    }
}