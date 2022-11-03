using Hermes.BuildingBlock.Object;
using Hermes.Client.Contract;

namespace Hermes.Client.Object
{
    public class Node : INode
    {
        public string Direction { get => direction; }
        private readonly string direction = string.Empty;
        public BlockChain Blockchain { get => Blockchain; }
        private readonly BlockChain blockchain = new();

        public bool IsOnline { get => online; }
        private bool online = false;

        public async Task<float> Response(int hash)
        {
            var nKeys = blockchain.Nodes.Keys.ToArray();
            var eIndex = Array.IndexOf(nKeys, Direction);
            blockchain.Nodes.TryGetValue(nKeys[eIndex - 1], out var nLeft);
            blockchain.Nodes.TryGetValue(nKeys[eIndex + 1], out var nRight);
            float a = 0f, b = 0f;
            if (nLeft != null) a = await nLeft.Response(hash);
            if (nRight != null) b = await nRight.Response(hash);
            return (a + b) * 0.5f;
        }
        public async Task<float> AddBlock(Block block, bool vote)
        {
            blockchain.Blocks.Add(block.ToString(), block);
            return await Response(block.GetHashCode());
        }
        public async Task<float> RegisterNode(Node node, bool vote)
        {
            return await Response(node.GetHashCode());
        }
        public async Task<float> LogoffNode(Node node, bool vote)
        {
            return await Response(node.GetHashCode());
        }
        public async Task<float> LoginNode(Node node, bool vote)
        {
            return await Response(node.GetHashCode());
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string? ToString()
        {
            return base.ToString();
        }
    }
}