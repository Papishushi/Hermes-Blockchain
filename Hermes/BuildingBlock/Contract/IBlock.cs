using Hermes.BuildingBlock.Data;
using Hermes.BuildingBlock.Enum;

namespace Hermes.BuildingBlock.Contract
{
    public interface IBlock
    {
        HashSet<DataTransfer> Body { get; }
        IBlock? GenesisBlock { get; }
        byte[] MerkleHash { get; }
        byte[] PreviousBlockHash { get; }
        DateTime TimeStamp { get; }
        BlockChainVersion Version { get; }

        bool AddDataTransfer(DataTransfer transfer);
        bool Equals(object? obj);
        int GetHashCode();
        bool RemoveDataTransfer(DataTransfer transfer);
        string ToString();

        public static bool operator +(IBlock b, DataTransfer t) => b.AddDataTransfer(t);
        public static bool operator -(IBlock b, DataTransfer t) => b.RemoveDataTransfer(t);
    }
}