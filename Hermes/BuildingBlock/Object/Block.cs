using Hermes.BuildingBlock.Contract;
using Hermes.BuildingBlock.Data;
using Hermes.BuildingBlock.Enum;
using Merkle.Data;
using System.Security.Cryptography;

namespace Hermes.BuildingBlock.Object
{
    public class Block : IBlock
    {
        public const int BLOCK_SIZE = 10000;

        public IBlock? GenesisBlock { get => genesisBlock; }
        private readonly IBlock? genesisBlock;
        public BlockChainVersion Version { get => version; }
        private readonly BlockChainVersion version = BlockChainVersion.FOUR;
        public byte[] PreviousBlockHash { get => previousBlockHash; }
        private readonly byte[] previousBlockHash;
        public DateTime TimeStamp { get => timeStamp; }
        private readonly DateTime timeStamp = DateTime.UtcNow;
        public HashSet<DataTransfer> Body { get => body; }
        private readonly HashSet<DataTransfer> body = new(BLOCK_SIZE);
        public byte[] MerkleHash { get => new MerkleTree<DataTransfer>(Body.ToArray()); }

        private Block()
        {
            genesisBlock = null;
            previousBlockHash = Array.Empty<byte>();
        }
        public Block(IBlockChain chain, byte[] previousBlockHash)
        {
            genesisBlock = chain.Blocks[GetLocalGenesisBlockID()];
            this.previousBlockHash = previousBlockHash;
        }

        private static string GetLocalGenesisBlockID() => new Block().ToString();

        public bool AddDataTransfer(DataTransfer transfer)
        {
            if (Body.Count == BLOCK_SIZE) return false;
            Body.Add(transfer);
            return true;
        }
        public bool RemoveDataTransfer(DataTransfer transfer)
        {
            if (Body.Count == 0) return false;
            Body.Remove(transfer);
            return true;
        }

        public static implicit operator byte[](Block block) => BitConverter.GetBytes(block.GetHashCode());
        public static implicit operator int(Block block) => block.GetHashCode();
        public static implicit operator string(Block block) => block.GetHashCode().ToString("x");

        public override bool Equals(object? obj) => obj is Block block && block.GetHashCode() == GetHashCode();
        public override int GetHashCode()
        {
            var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);

            if (hasher == null) return 0;
            hasher.AppendData(MerkleHash);
            hasher.AppendData(PreviousBlockHash);
            hasher.AppendData(BitConverter.GetBytes((byte)Version));
            if (GenesisBlock != null) hasher.AppendData((Block)GenesisBlock);

            return BitConverter.ToInt32(hasher.GetCurrentHash());
        }
        public override string ToString() => GetHashCode().ToString("x");
    }
}