
using Hermes.BuildingBlock.Enum;
using Hermes.Client.Contract;

namespace Hermes.BuildingBlock.Contract
{
    public interface IBlockChain
    {
        Dictionary<string, IBlock> Blocks { get; }
        Dictionary<string, INode> Nodes { get; }
        BlockChainVersion Version { get; }
    }
}