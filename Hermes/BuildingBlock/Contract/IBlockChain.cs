
using Hermes.BuildingBlock.Enum;
using Hermes.Client.Contract;
using System.Collections;

namespace Hermes.BuildingBlock.Contract
{
    public interface IBlockChain
    {
        Hashtable Blocks { get; }
        Hashtable Nodes { get; }
        BlockChainVersion Version { get; }
    }
}