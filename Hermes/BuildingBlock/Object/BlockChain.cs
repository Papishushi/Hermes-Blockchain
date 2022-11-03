using Hermes.BuildingBlock.Contract;
using Hermes.BuildingBlock.Enum;
using Hermes.Client.Contract;

namespace Hermes.BuildingBlock.Object
{
    public class BlockChain : IBlockChain
    {
        public BlockChainVersion Version { get => version; }
        private readonly BlockChainVersion version = BlockChainVersion.FOUR;
        public Dictionary<string, IBlock> Blocks { get => blocks; }
        private readonly Dictionary<string, IBlock> blocks = new();
        public Dictionary<string, INode> Nodes { get => nodes; }
        private readonly Dictionary<string, INode> nodes = new();
    }
}