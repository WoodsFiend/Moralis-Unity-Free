namespace MoralisSDK
{
    using Newtonsoft.Json;
    using System;
    using System.Runtime.Serialization;
    using UnityEngine;

    //Enums
    public enum EvmChain
    {
        ETH, SEPOLIA, HOLESKY, POLYGON, POLYGON_AMOY, BSC, BSC_TESTNET, AVALANCHE, FANTOM, PALM, CRONOS, ARBITRUM, GNOSIS, GNOSIS_TESTNET, CHILIZ, CHILIZ_TESTNET, BASE, BASE_SEPOLIA, OPTIMISM, OPTIMISM_SEPOLIA, LINEA, LINEA_SEPOLIA, MOONBEAM, MOONRIVER, MOONBASE
    }

    public enum EvmExchange
    {
        ANY, UNISWAPV2, UNISWAPV3, SUSHISWAPV2, PANCAKESWAPV1, PANCAKESWAPV2, PANCAKESWAPV3, QUICKSWAP, QUICKSWAPV2, TRADERJOE, PANGOLIN, SPOOKYSWAP, VVS, MM_FINANCE, CRODEX, CAMELOTV2
    }
    public enum ResyncType
    {
        uri,
        metadata
    }
    public enum ResyncMode
    {
        async,
        sync
    }
    public enum Order
    {
        DESC,
        ASC
    }


    namespace MoralisObjects
    {
        //Request/Response
        public class RequestError
        {
            [JsonProperty("message")]
            public string Message;
        }
        public class TokenRequest
        {
            [JsonProperty("token_address", NullValueHandling = NullValueHandling.Ignore)]
            public string TokenAddress;
            [JsonProperty("token_id", NullValueHandling = NullValueHandling.Ignore)]
            public string TokenId;
            [JsonProperty("exchange", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(LowercaseEnumConverter))] // Apply custom converter to the enum property
            public EvmExchange? Exchange;
            [JsonProperty("to_block", NullValueHandling = NullValueHandling.Ignore)]
            public string ToBlock;
        }

        //NFT
        public class NFT
        {
            [JsonProperty("amount")]
            public string Amount { get; set; }

            [JsonProperty("token_id")]
            public string TokenId { get; set; }

            [JsonProperty("token_address")]
            public string TokenAddress { get; set; }

            [JsonProperty("contract_type")]
            public string ContractType { get; set; }

            [JsonProperty("owner_of")]
            public string OwnerOf { get; set; }

            [JsonProperty("last_metadata_sync")]
            public DateTime? LastMetadataSync { get; set; }

            [JsonProperty("last_token_uri_sync")]
            public DateTime? LastTokenUriSync { get; set; }

            [JsonProperty("metadata")]
            public string MetadataSerialized { get; set; }

            [JsonProperty("notMetadata")]
            public NFTMetadata Metadata { get; private set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_number_minted")]
            public string BlockNumberMinted { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("token_hash")]
            public string TokenHash { get; set; }

            [JsonProperty("token_uri")]
            public string TokenUri { get; set; }

            [JsonProperty("minter_address")]
            public string MinterAddress { get; set; }

            [JsonProperty("verified_collection")]
            public bool VerifiedCollection { get; set; }

            [JsonProperty("possible_spam")]
            public bool PossibleSpam { get; set; }

            [JsonProperty("collection_logo")]
            public string CollectionLogo { get; set; }

            [JsonProperty("collection_banner_image")]
            public string CollectionBannerImage { get; set; }

            [OnDeserialized]
            internal void OnDeserializedMethod(StreamingContext context)
            {
                if (MetadataSerialized != null)
                {
                    try
                    {
                        Metadata = JsonConvert.DeserializeObject<NFTMetadata>(MetadataSerialized);
                    }
                    catch(Exception e)
                    {
                        Debug.LogWarning("Incompatible metadata from NFT\nToken Address: " + TokenAddress + "\nId: " + TokenId);
                    }
                }
            }
        }
        public class NFTMetadata
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("image")]
            public string Image { get; set; }

            [JsonProperty("animation_url")]
            public string AnimationUrl { get; set; }

            [JsonProperty("attributes")]
            public NFTAttribute[] Attributes { get; set; }
        }
        public class NFTAttribute
        {
            [JsonProperty("display_type")]
            public string DisplayType;

            [JsonProperty("trait_type")]
            public string TraitType;

            [JsonProperty("value")]
            public object Value; // Use object type to accommodate different value types
        }
        public class NFTBalance
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("page_size")]
            public int PageSize { get; set; }

            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("result")]
            public NFT[] Result { get; set; }
        }
        public class NFTWalletCollections
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("page")]
            public string Page { get; set; }

            [JsonProperty("page_size")]
            public string PageSize { get; set; }

            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("result")]
            public NFTContractMetadata[] Result { get; set; }
        }
        public class NFTContractMetadata
        {
            [JsonProperty("token_address")]
            public string TokenAddress { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("contract_type")]
            public string ContractType { get; set; }

            [JsonProperty("synced_at")]
            public DateTime? SyncedAt { get; set; }

            [JsonProperty("possible_spam")]
            public bool PossibleSpam { get; set; }

            [JsonProperty("verified_collection")]
            public bool VerifiedCollection { get; set; }

            [JsonProperty("collection_logo")]
            public string CollectionLogo { get; set; }

            [JsonProperty("collection_banner_image")]
            public string CollectionBannerImage { get; set; }

            [JsonProperty("collection_category")]
            public string CollectionCategory { get; set; }

            [JsonProperty("project_url")]
            public string ProjectUrl { get; set; }

            [JsonProperty("wiki_url")]
            public string WikiUrl { get; set; }

            [JsonProperty("discord_url")]
            public string DiscordUrl { get; set; }

            [JsonProperty("telegram_url")]
            public string TelegramUrl { get; set; }

            [JsonProperty("twitter_username")]
            public string TwitterUsername { get; set; }

            [JsonProperty("instagram_username")]
            public string InstagramUsername { get; set; }
        }
        public class NFTTrades
        {
            [JsonProperty("page")]
            public string Page { get; set; }

            [JsonProperty("page_size")]
            public string PageSize { get; set; }

            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("result")]
            public NFTTrade[] Result { get; set; }
        }
        public class NFTTrade
        {
            [JsonProperty("transaction_hash")]
            public string TransactionHash { get; set; }

            [JsonProperty("transaction_index")]
            public string TransactionIndex { get; set; }

            [JsonProperty("token_ids")]
            public string[] TokenIds { get; set; }

            [JsonProperty("seller_address")]
            public string SellerAddress { get; set; }

            [JsonProperty("buyer_address")]
            public string BuyerAddress { get; set; }

            [JsonProperty("marketplace_address")]
            public string MarketplaceAddress { get; set; }

            [JsonProperty("price")]
            public string Price { get; set; }

            [JsonProperty("price_token_address")]
            public string PriceTokenAddress { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_hash")]
            public string BlockHash { get; set; }

            [JsonProperty("verified_collection")]
            public string VerifiedCollection { get; set; }
        }
        public class NFTTransferData
        {
            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("page_size")]
            public int PageSize { get; set; }

            [JsonProperty("cursor")]
            public object Cursor { get; set; }

            [JsonProperty("result")]
            public NFTTransferResult[] Result { get; set; }

            [JsonProperty("block_exists")]
            public bool BlockExists { get; set; }
        }
        public class NFTTransferResult
        {
            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("block_hash")]
            public string BlockHash { get; set; }

            [JsonProperty("transaction_hash")]
            public string TransactionHash { get; set; }

            [JsonProperty("transaction_index")]
            public int TransactionIndex { get; set; }

            [JsonProperty("log_index")]
            public int LogIndex { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("contract_type")]
            public string ContractType { get; set; }

            [JsonProperty("transaction_type")]
            public string TransactionType { get; set; }

            [JsonProperty("token_address")]
            public string TokenAddress { get; set; }

            [JsonProperty("token_id")]
            public string TokenId { get; set; }

            [JsonProperty("from_address")]
            public string FromAddress { get; set; }

            [JsonProperty("from_address_label")]
            public string FromAddressLabel { get; set; }

            [JsonProperty("to_address")]
            public string ToAddress { get; set; }

            [JsonProperty("to_address_label")]
            public string ToAddressLabel { get; set; }

            [JsonProperty("amount")]
            public string Amount { get; set; }

            [JsonProperty("verified")]
            public int Verified { get; set; }

            [JsonProperty("operator")]
            public string Operator { get; set; }

            [JsonProperty("possible_spam")]
            public bool PossibleSpam { get; set; }

            [JsonProperty("verified_collection")]
            public bool VerifiedCollection { get; set; }
        }
        public class OwnershipTransferData
        {
            [JsonProperty("owners")]
            public OwnershipData Owners { get; set; }

            [JsonProperty("transfers")]
            public TotalValue Transfers { get; set; }
        }
        public class OwnershipData
        {
            [JsonProperty("current")]
            public string Current { get; set; }
        }

        //Token
        public class NativeToken
        {
            [JsonProperty("balance")]
            public string Balance { get; set; }
        }
        public class ERC20Token
        {
            [JsonProperty("token_address")]
            public string TokenAddress { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("logo")]
            public string Logo { get; set; }

            [JsonProperty("thumbnail")]
            public string Thumbnail { get; set; }

            [JsonProperty("decimals")]
            public int? Decimals { get; set; }

            [JsonProperty("balance")]
            public string Balance { get; set; }

            [JsonProperty("possible_spam")]
            public string PossibleSpam { get; set; }

            [JsonProperty("verified_collection")]
            public string VerifiedCollection { get; set; }

            [JsonProperty("total_supply")]
            public string TotalSupply { get; set; }

            [JsonProperty("total_supply_formatted")]
            public string TotalSupplyFormatted { get; set; }

            [JsonProperty("percentage_relative_to_total_supply")]
            public double? PercentageRelativeToTotalSupply { get; set; }
        }
        public class ERC20WalletTransfers
        {
            [JsonProperty("total")]
            public string Total { get; set; }

            [JsonProperty("page")]
            public string Page { get; set; }

            [JsonProperty("page_size")]
            public string PageSize { get; set; }

            [JsonProperty("result")]
            public ERC20Transfer[] Result { get; set; }
        }
        public class ERC20Transfer
        {
            [JsonProperty("token_name")]
            public string TokenName { get; set; }

            [JsonProperty("token_symbol")]
            public string TokenSymbol { get; set; }

            [JsonProperty("token_logo")]
            public string TokenLogo { get; set; }

            [JsonProperty("token_decimals")]
            public string TokenDecimals { get; set; }

            [JsonProperty("transaction_hash")]
            public string TransactionHash { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_hash")]
            public string BlockHash { get; set; }

            [JsonProperty("to_address")]
            public string ToAddress { get; set; }

            [JsonProperty("to_address_label")]
            public string ToAddressLabel { get; set; }

            [JsonProperty("from_address")]
            public string FromAddress { get; set; }

            [JsonProperty("from_address_label")]
            public string FromAddressLabel { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("transaction_index")]
            public string TransactionIndex { get; set; }

            [JsonProperty("log_index")]
            public string LogIndex { get; set; }

            [JsonProperty("possible_spam")]
            public bool PossibleSpam { get; set; }

            [JsonProperty("verified_collection")]
            public bool VerifiedCollection { get; set; }
        }
        public class TokenData
        {
            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("address_label")]
            public string AddressLabel { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("decimals")]
            public string Decimals { get; set; }

            [JsonProperty("logo")]
            public string Logo { get; set; }

            [JsonProperty("logo_hash")]
            public string LogoHash { get; set; }

            [JsonProperty("thumbnail")]
            public string Thumbnail { get; set; }

            [JsonProperty("total_supply")]
            public string TotalSupply { get; set; }

            [JsonProperty("total_supply_formatted")]
            public string TotalSupplyFormatted { get; set; }

            [JsonProperty("fully_diluted_valuation")]
            public string FullyDilutedValuation { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("validated")]
            public int Validated { get; set; }

            [JsonProperty("created_at")]
            public DateTime? CreatedAt { get; set; }

            [JsonProperty("possible_spam")]
            public bool PossibleSpam { get; set; }

            [JsonProperty("verified_contract")]
            public bool VerifiedContract { get; set; }

            [JsonProperty("categories")]
            public string[] Categories { get; set; }

            [JsonProperty("links")]
            public Links Links { get; set; }
        }
        public class Links
        {
            [JsonProperty("twitter")]
            public string Twitter { get; set; }

            [JsonProperty("website")]
            public string Website { get; set; }

            [JsonProperty("reddit")]
            public string Reddit { get; set; }

            [JsonProperty("github")]
            public string Github { get; set; }

            [JsonProperty("telegram")]
            public string Telegram { get; set; }
        }
        public class TokenPrice
        {
            [JsonProperty("tokenName")]
            public string TokenName { get; set; }

            [JsonProperty("tokenSymbol")]
            public string TokenSymbol { get; set; }

            [JsonProperty("tokenLogo")]
            public string TokenLogo { get; set; }

            [JsonProperty("tokenDecimals")]
            public string TokenDecimals { get; set; }

            [JsonProperty("nativePrice")]
            public NativePrice NativePrice { get; set; }

            [JsonProperty("usdPrice")]
            public double? UsdPrice { get; set; }

            [JsonProperty("usdPriceFormatted")]
            public string UsdPriceFormatted { get; set; }

            [JsonProperty("exchangeName")]
            public string ExchangeName { get; set; }

            [JsonProperty("exchangeAddress")]
            public string ExchangeAddress { get; set; }

            [JsonProperty("tokenAddress")]
            public string TokenAddress { get; set; }

            [JsonProperty("priceLastChangedAtBlock")]
            public string PriceLastChangedAtBlock { get; set; }

            [JsonProperty("verifiedContract")]
            public bool VerifiedContract { get; set; }

            [JsonProperty("24hrPercentChange")]
            public double? TwentyFourHourPercentChange { get; set; }
        }
        public class NativePrice
        {
            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("decimals")]
            public int Decimals { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }
        }
        public class TokenOwnersInfo
        {
            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("page_size")]
            public int PageSize { get; set; }

            [JsonProperty("result")]
            public TokenOwner[] Result { get; set; }
        }
        public class TokenOwner
        {
            [JsonProperty("balance")]
            public string Balance { get; set; }

            [JsonProperty("balance_formatted")]
            public string BalanceFormatted { get; set; }

            [JsonProperty("is_contract")]
            public bool IsContract { get; set; }

            [JsonProperty("owner_address")]
            public string OwnerAddress { get; set; }

            [JsonProperty("owner_address_label")]
            public string OwnerAddressLabel { get; set; }

            [JsonProperty("usd_value")]
            public string UsdValue { get; set; }

            [JsonProperty("percentage_relative_to_total_supply")]
            public double? PercentageRelativeToTotalSupply { get; set; }
        }
        public class ERC20TokenStats
        {
            [JsonProperty("transfers")]
            public TotalValue Transfers;
        }

        //Defi
        public class PairReserve
        {
            [JsonProperty("reserve0")]
            public string Reserve0 { get; set; }

            [JsonProperty("reserve1")]
            public string Reserve1 { get; set; }
        }

        public class PairInfo
        {
            [JsonProperty("token0")]
            public TokenData Token0 { get; set; }

            [JsonProperty("token1")]
            public TokenData Token1 { get; set; }

            [JsonProperty("pairAddress")]
            public string PairAddress { get; set; }
        }

        //Block
        public class BlockData
        {
            [JsonProperty("timestamp")]
            public DateTime? Timestamp { get; set; }

            [JsonProperty("number")]
            public int Number { get; set; }

            [JsonProperty("hash")]
            public string Hash { get; set; }

            [JsonProperty("parent_hash")]
            public string ParentHash { get; set; }

            [JsonProperty("nonce")]
            public string Nonce { get; set; }

            [JsonProperty("sha3_uncles")]
            public string Sha3Uncles { get; set; }

            [JsonProperty("logs_bloom")]
            public string LogsBloom { get; set; }

            [JsonProperty("transactions_root")]
            public string TransactionsRoot { get; set; }

            [JsonProperty("state_root")]
            public string StateRoot { get; set; }

            [JsonProperty("receipts_root")]
            public string ReceiptsRoot { get; set; }

            [JsonProperty("miner")]
            public string Miner { get; set; }

            [JsonProperty("difficulty")]
            public string Difficulty { get; set; }

            [JsonProperty("total_difficulty")]
            public string TotalDifficulty { get; set; }

            [JsonProperty("size")]
            public string Size { get; set; }

            [JsonProperty("extra_data")]
            public string ExtraData { get; set; }

            [JsonProperty("gas_limit")]
            public string GasLimit { get; set; }

            [JsonProperty("gas_used")]
            public string GasUsed { get; set; }

            [JsonProperty("transaction_count")]
            public string TransactionCount { get; set; }

            [JsonProperty("transactions")]
            public Transaction[] Transactions { get; set; }
        }
        public class NativeWalletTransactions
        {
            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("page_size")]
            public int PageSize { get; set; }

            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("result")]
            public Transaction[] Result { get; set; }
        }
        public class Transaction
        {
            [JsonProperty("hash")]
            public string Hash { get; set; }

            [JsonProperty("nonce")]
            public string Nonce { get; set; }

            [JsonProperty("transaction_index")]
            public string TransactionIndex { get; set; }

            [JsonProperty("from_address")]
            public string FromAddress { get; set; }

            [JsonProperty("to_address")]
            public string ToAddress { get; set; }

            [JsonProperty("from_address_label")]
            public string FromAddressLabel { get; set; }

            [JsonProperty("to_address_label")]
            public string ToAddressLabel { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("gas")]
            public string Gas { get; set; }

            [JsonProperty("gas_price")]
            public string GasPrice { get; set; }

            [JsonProperty("input")]
            public string Input { get; set; }

            [JsonProperty("receipt_cumulative_gas_used")]
            public string ReceiptCumulativeGasUsed { get; set; }

            [JsonProperty("receipt_gas_used")]
            public string ReceiptGasUsed { get; set; }

            [JsonProperty("receipt_contract_address")]
            public string ReceiptContractAddress { get; set; }

            [JsonProperty("receipt_root")]
            public string ReceiptRoot { get; set; }

            [JsonProperty("receipt_status")]
            public string ReceiptStatus { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_hash")]
            public string BlockHash { get; set; }

            [JsonProperty("logs")]
            public Log[] Logs { get; set; }

            [JsonProperty("decoded_call")]
            public DecodedCall DecodedCall { get; set; }

            [JsonProperty("transaction_fee")]
            public string TransactionFee { get; set; }

            [JsonProperty("internal_transactions")]
            public InternalTransaction InternalTransactions { get; set; }
        }
        public class Log
        {
            [JsonProperty("log_index")]
            public string LogIndex { get; set; }

            [JsonProperty("transaction_hash")]
            public string TransactionHash { get; set; }

            [JsonProperty("transaction_index")]
            public string TransactionIndex { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("data")]
            public string Data { get; set; }

            [JsonProperty("topic0")]
            public string Topic0 { get; set; }

            [JsonProperty("topic1")]
            public string Topic1 { get; set; }

            [JsonProperty("topic2")]
            public string Topic2 { get; set; }

            [JsonProperty("topic3")]
            public string Topic3 { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_hash")]
            public string BlockHash { get; set; }
            
            [JsonProperty("transfer_index")]
            public string[] TransferIndex { get; set; }

            [JsonProperty("transaction_value")]
            public string TransactionValue { get; set; }

            [JsonProperty("decoded_event")]
            public DecodedEvent DecodedEvent { get; set; }

        }
        public class DecodedEvent
        {
            [JsonProperty("signature")]
            public string Signature { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("params")]
            public EventParam[] Params { get; set; }
        }
        public class DecodedCall
        {
            [JsonProperty("signature")]
            public string Signature { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("params")]
            public EventParam[] Params { get; set; }
        }
        public class EventParam
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }
        public class InternalTransaction
        {
            [JsonProperty("transaction_hash")]
            public string TransactionHash { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_hash")]
            public string BlockHash { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("from")]
            public string From { get; set; }

            [JsonProperty("to")]
            public string To { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("gas")]
            public string Gas { get; set; }

            [JsonProperty("gas_used")]
            public string GasUsed { get; set; }

            [JsonProperty("input")]
            public string Input { get; set; }

            [JsonProperty("output")]
            public string Output { get; set; }
        }
        public class BlockInfo
        {
            [JsonProperty("block")]
            public long? Block { get; set; }

            [JsonProperty("date")]
            public DateTime? Date { get; set; }

            [JsonProperty("timestamp")]
            public long Timestamp { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("hash")]
            public string Hash { get; set; }

            [JsonProperty("parent_hash")]
            public string ParentHash { get; set; }
        }
        public class BlockStats
        {
            [JsonProperty("nfts")]
            public string NFTs { get; set; }
            [JsonProperty("collections")]
            public string Collections { get; set; }

            [JsonProperty("transactions")]
            public TotalValue Transactions { get; set; }

            [JsonProperty("nft_transfers")]
            public TotalValue NftTransfers { get; set; }

            [JsonProperty("token_transfers")]
            public TotalValue TokenTransfers { get; set; }
        }

        public class TotalValue
        {
            [JsonProperty("total")]
            public string Total { get; set; }
        }

        //Event
        public class ContractLogs
        {
            [JsonProperty("page_size")]
            public int PageSize { get; set; }

            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("result")]
            public Log[] Result { get; set; }
        }

        public class ContractEvents
        {
            [JsonProperty("page_size")]
            public int PageSize { get; set; }

            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("result")]
            public EventResult[] Result { get; set; }
        }

        public class EventResult
        {
            [JsonProperty("transaction_hash")]
            public string TransactionHash { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_hash")]
            public string BlockHash { get; set; }

            [JsonProperty("data")]
            public EventData Data { get; set; }
        }

        public class EventData
        {
            [JsonProperty("src")]
            public string Src { get; set; }

            [JsonProperty("dst")]
            public string Dst { get; set; }

            [JsonProperty("wad")]
            public string Wad { get; set; }
        }

        //Wallets
        public class TransactionData
        {
            [JsonProperty("page_size")]
            public int PageSize { get; set; }

            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("result")]
            public TransactionDataResult[] Result { get; set; }
        }

        public class TransactionDataResult
        {
            [JsonProperty("hash")]
            public string Hash { get; set; }

            [JsonProperty("nonce")]
            public string Nonce { get; set; }

            [JsonProperty("transaction_index")]
            public string TransactionIndex { get; set; }

            [JsonProperty("from_address")]
            public string FromAddress { get; set; }

            [JsonProperty("from_address_label")]
            public string FromAddressLabel { get; set; }

            [JsonProperty("to_address")]
            public string ToAddress { get; set; }

            [JsonProperty("to_address_label")]
            public string ToAddressLabel { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("gas")]
            public string Gas { get; set; }

            [JsonProperty("gas_price")]
            public string GasPrice { get; set; }

            [JsonProperty("input")]
            public string Input { get; set; }

            [JsonProperty("receipt_cumulative_gas_used")]
            public string ReceiptCumulativeGasUsed { get; set; }

            [JsonProperty("receipt_gas_used")]
            public string ReceiptGasUsed { get; set; }

            [JsonProperty("receipt_contract_address")]
            public string ReceiptContractAddress { get; set; }

            [JsonProperty("receipt_root")]
            public string ReceiptRoot { get; set; }

            [JsonProperty("receipt_status")]
            public string ReceiptStatus { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_hash")]
            public string BlockHash { get; set; }

            [JsonProperty("logs")]
            public Log[] Logs { get; set; }

            [JsonProperty("internal_transactions")]
            public InternalTransaction[] InternalTransactions { get; set; }

            [JsonProperty("nft_transfers")]
            public NFTTransferResult[] NftTransfers { get; set; }

            [JsonProperty("erc20_transfer")]
            public ERC20Transfer[] Erc20Transfers { get; set; }

            [JsonProperty("native_transfers")]
            public NativeTransfer[] NativeTransfers { get; set; }
        }
        public class NativeTransfer
        {
            [JsonProperty("from_address")]
            public string FromAddress { get; set; }

            [JsonProperty("from_address_label")]
            public string FromAddressLabel { get; set; }

            [JsonProperty("to_address")]
            public string ToAddress { get; set; }

            [JsonProperty("to_address_label")]
            public string ToAddressLabel { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("value_formatted")]
            public string ValueFormatted { get; set; }

            [JsonProperty("direction")]
            public string Direction { get; set; }

            [JsonProperty("internal_transaction")]
            public string InternalTransaction { get; set; }

            [JsonProperty("token_symbol")]
            public string TokenSymbol { get; set; }

            [JsonProperty("token_logo")]
            public string TokenLogo { get; set; }
        }

        public class NetWorthData
        {
            [JsonProperty("total_networth_usd")]
            public string TotalNetworthUsd { get; set; }

            [JsonProperty("chains")]
            public ChainNetWorth[] Chains { get; set; }
        }

        public class ChainNetWorth
        {
            [JsonProperty("chain")]
            public string Chain { get; set; }

            [JsonProperty("native_balance")]
            public string NativeBalance { get; set; }

            [JsonProperty("native_balance_formatted")]
            public string NativeBalanceFormatted { get; set; }

            [JsonProperty("native_balance_usd")]
            public string NativeBalanceUsd { get; set; }

            [JsonProperty("token_balance_usd")]
            public string TokenBalanceUsd { get; set; }

            [JsonProperty("networth_usd")]
            public string NetworthUsd { get; set; }
        }
        public class ChainBalanceData
        {
            [JsonProperty("chain")]
            public string Chain { get; set; }

            [JsonProperty("chain_id")]
            public string ChainId { get; set; }

            [JsonProperty("total_balance")]
            public string TotalBalance { get; set; }

            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_timestamp")]
            public string BlockTimestamp { get; set; }

            [JsonProperty("total_balance_formatted")]
            public string TotalBalanceFormatted { get; set; }

            [JsonProperty("wallet_balances")]
            public WalletBalance[] WalletBalances { get; set; }
        }

        public class WalletBalance
        {
            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("balance")]
            public string Balance { get; set; }

            [JsonProperty("balance_formatted")]
            public string BalanceFormatted { get; set; }
        }
        public class TokenBalancesPriceResult
        {
            [JsonProperty("token_address")]
            public string TokenAddress { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("logo")]
            public string Logo { get; set; }

            [JsonProperty("thumbnail")]
            public string Thumbnail { get; set; }

            [JsonProperty("decimals")]
            public int? Decimals { get; set; }

            [JsonProperty("balance")]
            public string Balance { get; set; }

            [JsonProperty("possible_spam")]
            public bool PossibleSpam { get; set; }

            [JsonProperty("verified_contract")]
            public bool VerifiedContract { get; set; }

            [JsonProperty("total_supply")]
            public string TotalSupply { get; set; }

            [JsonProperty("total_supply_formatted")]
            public string TotalSupplyFormatted { get; set; }

            [JsonProperty("percentage_relative_to_total_supply")]
            public double? PercentageRelativeToTotalSupply { get; set; }

            [JsonProperty("balance_formatted")]
            public string BalanceFormatted { get; set; }

            [JsonProperty("usd_price")]
            public double? UsdPrice { get; set; }

            [JsonProperty("usd_price_24hr_percent_change")]
            public double? UsdPrice24HrPercentChange { get; set; }

            [JsonProperty("usd_price_24hr_usd_change")]
            public double? UsdPrice24HrUsdChange { get; set; }

            [JsonProperty("usd_value")]
            public double? UsdValue { get; set; }

            [JsonProperty("usd_value_24hr_usd_change")]
            public double? UsdValue24HrUsdChange { get; set; }

            [JsonProperty("native_token")]
            public bool NativeToken { get; set; }

            [JsonProperty("portfolio_percentage")]
            public double? PortfolioPercentage { get; set; }
        }

        public class TokenBalancesPrice
        {
            [JsonProperty("cursor")]
            public string Cursor { get; set; }

            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("page_size")]
            public int PageSize { get; set; }

            [JsonProperty("result")]
            public TokenBalancesPriceResult[] Result { get; set; }
        }
        public class WalletActiveChains
        {
            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("active_chains")]
            public ActiveChain[] ActiveChains { get; set; }
        }

        public class ActiveChain
        {
            [JsonProperty("chain")]
            public string Chain { get; set; }

            [JsonProperty("chain_id")]
            public string ChainId { get; set; }

            [JsonProperty("first_transaction")]
            public TransactionDetails FirstTransaction { get; set; }

            [JsonProperty("last_transaction")]
            public TransactionDetails LastTransaction { get; set; }
        }

        public class TransactionDetails
        {
            [JsonProperty("block_number")]
            public string BlockNumber { get; set; }

            [JsonProperty("block_timestamp")]
            public DateTime? BlockTimestamp { get; set; }

            [JsonProperty("transaction_hash")]
            public string TransactionHash { get; set; }
        }
    }

    //Converter for lowercase enums
    public class LowercaseEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                string enumString = reader.Value.ToString();
                return Enum.Parse(objectType, enumString, true); // Ignore case during parsing
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                int enumInt = Convert.ToInt32(reader.Value);
                return Enum.ToObject(objectType, enumInt);
            }

            throw new JsonSerializationException("Unexpected token type: " + reader.TokenType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType().IsEnum)
            {
                string enumString = value.ToString().ToLower(); // Convert enum value to lowercase string
                writer.WriteValue(enumString);
            }
            else
            {
                throw new JsonSerializationException("Expected enum type, got " + value.GetType());
            }
        }
    }
}