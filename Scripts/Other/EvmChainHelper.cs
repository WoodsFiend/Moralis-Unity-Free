using System;
using System.Collections.Generic;

namespace MoralisSDK.Tools
{
    //Helpers to translate between hex chain ID and Moralis EvmChain
    //Helpers get various information about a chain such as chain name, token symbol, rpc urls, etc...
    public class EvmChainHelper
    {
        private static readonly Dictionary<string, EvmChain> chainIdMap = new Dictionary<string, EvmChain>
        {
            { "0x1", EvmChain.ETH },
            { "0xaa36a7", EvmChain.SEPOLIA },
            { "0x4268", EvmChain.HOLESKY },
            { "0x89", EvmChain.POLYGON },
            { "0x13882", EvmChain.POLYGON_AMOY },
            { "0x38", EvmChain.BSC },
            { "0x61", EvmChain.BSC_TESTNET },
            { "0xa4b1", EvmChain.ARBITRUM },
            { "0x2105", EvmChain.BASE },
            { "0x14a34", EvmChain.BASE_SEPOLIA },
            { "0xa", EvmChain.OPTIMISM },
            { "0xaa37dc", EvmChain.OPTIMISM_SEPOLIA },
            { "0xe708", EvmChain.LINEA },
            { "0xe705", EvmChain.LINEA_SEPOLIA },
            { "0xa86a", EvmChain.AVALANCHE },
            { "0xfa", EvmChain.FANTOM },
            { "0x19", EvmChain.CRONOS },
            { "0x2a15c308d", EvmChain.PALM },
            { "0x64", EvmChain.GNOSIS },
            { "0x27d8", EvmChain.GNOSIS_TESTNET },
            { "0x15b38", EvmChain.CHILIZ },
            { "0x15b32", EvmChain.CHILIZ_TESTNET },
            { "0x504", EvmChain.MOONBEAM },
            { "0x505", EvmChain.MOONRIVER },
            { "0x507", EvmChain.MOONBASE }
        };
        private static readonly Dictionary<EvmChain, string> chainTokenSymbolMap = new Dictionary<EvmChain, string>
        {
            { EvmChain.ETH, "ETH" },
            { EvmChain.SEPOLIA, "ETH" },
            { EvmChain.HOLESKY, "ETH" },
            { EvmChain.POLYGON, "MATIC" },
            { EvmChain.POLYGON_AMOY, "MATIC" },
            { EvmChain.BSC, "BNB" },
            { EvmChain.BSC_TESTNET, "BNB" },
            { EvmChain.ARBITRUM, "ETH" },
            { EvmChain.BASE, "ETH" },
            { EvmChain.BASE_SEPOLIA, "ETH" },
            { EvmChain.OPTIMISM, "ETH" },
            { EvmChain.OPTIMISM_SEPOLIA, "ETH" },
            { EvmChain.LINEA, "ETH" },
            { EvmChain.LINEA_SEPOLIA, "ETH" },
            { EvmChain.AVALANCHE, "AVAX" },
            { EvmChain.FANTOM, "FTM" },
            { EvmChain.CRONOS, "CRO" },
            { EvmChain.PALM, "PALM" },
            { EvmChain.GNOSIS, "xDAI" },
            { EvmChain.GNOSIS_TESTNET, "xDAI" },
            { EvmChain.CHILIZ, "CHZ" },
            { EvmChain.CHILIZ_TESTNET, "CHZ" },
            { EvmChain.MOONBEAM, "GLMR" },
            { EvmChain.MOONRIVER, "MOVR" },
            { EvmChain.MOONBASE, "DEV" }
        };

        private static readonly Dictionary<EvmChain, string> chainNameMap = new Dictionary<EvmChain, string>
        {
            { EvmChain.ETH, "Ethereum Mainnet" },
            { EvmChain.SEPOLIA, "Ethereum Sepolia" },
            { EvmChain.HOLESKY, "Ethereum Holesky" },
            { EvmChain.POLYGON, "Polygon Mainnet" },
            { EvmChain.POLYGON_AMOY, "Polygon Amoy" },
            { EvmChain.BSC, "Binance Smart Chain Mainnet" },
            { EvmChain.BSC_TESTNET, "Binance Smart Chain Testnet" },
            { EvmChain.ARBITRUM, "Arbitrum" },
            { EvmChain.BASE, "Base" },
            { EvmChain.BASE_SEPOLIA, "Base Sepolia" },
            { EvmChain.OPTIMISM, "Optimism" },
            { EvmChain.OPTIMISM_SEPOLIA, "Optimism Sepolia" },
            { EvmChain.LINEA, "Linea" },
            { EvmChain.LINEA_SEPOLIA, "Linea Sepolia" },
            { EvmChain.AVALANCHE, "Avalanche" },
            { EvmChain.FANTOM, "Fantom" },
            { EvmChain.CRONOS, "Cronos" },
            { EvmChain.PALM, "Palm" },
            { EvmChain.GNOSIS, "Gnosis" },
            { EvmChain.GNOSIS_TESTNET, "Gnosis Testnet" },
            { EvmChain.CHILIZ, "Chiliz" },
            { EvmChain.CHILIZ_TESTNET, "Chiliz Testnet" },
            { EvmChain.MOONBEAM, "Moonbeam" },
            { EvmChain.MOONRIVER, "Moonriver" },
            { EvmChain.MOONBASE, "Moonbase" }
        };
        private static readonly Dictionary<EvmChain, string[]> chainRpcsMap = new Dictionary<EvmChain, string[]>
        {
            { EvmChain.ETH, new string[] { "https://ethereum-rpc.publicnode.com" } },
            { EvmChain.SEPOLIA, new string[] { "https://ethereum-sepolia-rpc.publicnode.com" } },
            { EvmChain.HOLESKY, new string[] { "https://ethereum-holesky-rpc.publicnode.com" } },
            { EvmChain.POLYGON, new string[] { "https://polygon-bor-rpc.publicnode.com" } },
            { EvmChain.POLYGON_AMOY, new string[] { "https://polygon-amoy-bor-rpc.publicnode.com" } },
            { EvmChain.BSC, new string[] { "https://bsc-dataseed.binance.org/" } },
            { EvmChain.BSC_TESTNET, new string[] { "https://bsc-testnet-rpc.publicnode.com" } },
            { EvmChain.ARBITRUM, new string[] { "https://arbitrum-one-rpc.publicnode.com" } },
            { EvmChain.BASE, new string[] { "https://base-rpc.publicnode.com" } },
            { EvmChain.BASE_SEPOLIA, new string[] { "https://base-sepolia-rpc.publicnode.com" } },
            { EvmChain.OPTIMISM, new string[] { "https://optimism-rpc.publicnode.com" } },
            { EvmChain.OPTIMISM_SEPOLIA, new string[] { "https://optimism-sepolia.drpc.org" } },
            { EvmChain.LINEA, new string[] { "https://linea.decubate.com" } },
            { EvmChain.LINEA_SEPOLIA, new string[] { "https://rpc.sepolia.linea.build" } },
            { EvmChain.AVALANCHE, new string[] { "https://avalanche-c-chain-rpc.publicnode.com" } },
            { EvmChain.FANTOM, new string[] { "https://rpc.fantom.network" } },
            { EvmChain.CRONOS, new string[] { "https://cronos-evm-rpc.publicnode.com" } },
            { EvmChain.PALM, new string[] { "https://palm-mainnet.public.blastapi.io" } },
            { EvmChain.GNOSIS, new string[] { "https://gnosis-rpc.publicnode.com" } },
            { EvmChain.GNOSIS_TESTNET, new string[] { "https://1rpc.io/gnosis" } },
            { EvmChain.CHILIZ, new string[] { "https://chiliz.publicnode.com" } },
            { EvmChain.CHILIZ_TESTNET, new string[] { "https://spicy-rpc.chiliz.com" } },
            { EvmChain.MOONBEAM, new string[] { "https://rpc.api.moonbeam.network" } },
            { EvmChain.MOONRIVER, new string[] { "https://moonriver-rpc.publicnode.com" } },
            { EvmChain.MOONBASE, new string[] { "https://rpc.api.moonbase.moonbeam.network" } }
        };

        public static EvmChain? GetChainFromId(string selectedChainId)
        {
            if (chainIdMap.TryGetValue(selectedChainId, out EvmChain evmChain))
            {
                return evmChain;
            }

            throw new ArgumentException($"Invalid chain ID: {selectedChainId}");
        }
        public static string GetChainIdFromChain(EvmChain evmChain)
        {
            foreach (var kvp in chainIdMap)
            {
                if (kvp.Value == evmChain)
                {
                    return kvp.Key;
                }
            }

            throw new ArgumentException($"Invalid EvmChain: {evmChain}");
        }
        public static string GetTokenSymbolFromChain(EvmChain chain)
        {
            if (chainTokenSymbolMap.TryGetValue(chain, out string tokenName))
            {
                return tokenName;
            }

            throw new ArgumentException($"Invalid chain for token name: {chain}");
        }
        public static string GetChainNameFromChain(EvmChain chain)
        {
            if (chainNameMap.TryGetValue(chain, out string name))
            {
                return name;
            }

            throw new ArgumentException($"Invalid chain: {chain}");
        }
        public static string[] GetChainRpcsFromChain(EvmChain chain)
        {
            if (chainRpcsMap.TryGetValue(chain, out string[] rpcs))
            {
                return rpcs;
            }

            throw new ArgumentException($"Invalid chain for Rpcs: {chain}");
        }
    }
}