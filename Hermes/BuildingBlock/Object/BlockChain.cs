using Hermes.BuildingBlock.Contract;
using Hermes.BuildingBlock.Enum;
using Hermes.Client.Contract;
using Merkle.Data;
using System.Collections;
using System.Security.Cryptography;

namespace Hermes.BuildingBlock.Object
{
    public class BlockChain : IBlockChain
    {
        public BlockChainVersion Version { get => version; }
        private readonly BlockChainVersion version = BlockChainVersion.FOUR;
        public Hashtable Blocks { get => blocks; }
        private readonly Hashtable blocks = new();
        public Hashtable Nodes { get => nodes; }
        private readonly Hashtable nodes = new();

        public static implicit operator byte[](BlockChain chain) => BitConverter.GetBytes(chain.GetHashCode());
        public static implicit operator int(BlockChain chain) => chain.GetHashCode();
        public static implicit operator string(BlockChain chain) => chain.GetHashCode().ToString("x");
        public override bool Equals(object? obj) => obj is BlockChain chain && chain.GetHashCode() == GetHashCode();
        public override int GetHashCode()
        {
            var tkeys = Array.Empty<string>();
            var tblocks = Array.Empty<IBlock>();
            var tnodes = Array.Empty<INode>();

            var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);

            if (hasher == null) return 0;

            blocks.Keys.CopyTo(tkeys, 0);
            hasher.AppendData(new MerkleTree<string>(tkeys));
            blocks.Values.CopyTo(tblocks, 0);
            hasher.AppendData(new MerkleTree<IBlock>(tblocks));
            tkeys = Array.Empty<string>();
            nodes.Keys.CopyTo(tkeys, 0);
            hasher.AppendData(new MerkleTree<string>(tkeys));
            nodes.Values.CopyTo(tnodes, 0);
            hasher.AppendData(new MerkleTree<INode>(tnodes));
            hasher.AppendData(BitConverter.GetBytes((byte)version));

            return BitConverter.ToInt32(hasher.GetCurrentHash());
        }
        public override string ToString() => GetHashCode().ToString("x");
    }
}