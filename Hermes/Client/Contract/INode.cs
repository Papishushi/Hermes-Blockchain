using Hermes.BuildingBlock.Object;
using Hermes.Client.Object;

namespace Hermes.Client.Contract
{
    public interface INode
    {
        BlockChain Blockchain { get; }
        string Direction { get; }
        bool IsOnline { get; }

        Task<float> AddBlock(Block block, bool vote);
        bool Equals(object? obj);
        int GetHashCode();
        Task<float> LoginNode(Node node, bool vote);
        Task<float> LogoffNode(Node node, bool vote);
        Task<float> RegisterNode(Node node, bool vote);
        Task<float> Response(int hash);
        string? ToString();
    }
}