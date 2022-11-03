using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Hermes.BuildingBlock.Data
{
    public struct DataTransfer
    {
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 16)]
        private readonly byte[] senderDirection;
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 16)]
        private readonly byte[] receiverDirection;
        [MarshalAs(UnmanagedType.LPArray)]
        private readonly byte[] content;
        private readonly byte[] hash;

        public DataTransfer(string senderDirection, string receiverDirection, object content)
        {
            this.senderDirection = Encoding.Unicode.GetBytes(senderDirection.ToCharArray(), 0, 16);
            this.receiverDirection = Encoding.Unicode.GetBytes(receiverDirection.ToCharArray(), 0, 16);
            unsafe
            {
                var size = Marshal.SizeOf(content);
                this.content = new byte[size];
                fixed (byte* buffer = this.content)
                    Marshal.StructureToPtr(content, (IntPtr)buffer, false);
            }
            var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
            hasher.AppendData(this.senderDirection);
            hasher.AppendData(this.receiverDirection);
            hasher.AppendData(this.content);
            hash = hasher.GetCurrentHash();
        }

        public static implicit operator byte[](DataTransfer transfer) => transfer.hash;
        public static implicit operator int(DataTransfer transfer) => BitConverter.ToInt32((byte[])transfer);
        public static implicit operator string(DataTransfer transfer) => ((int)transfer).ToString("x");

        public override bool Equals(object? obj) => obj is DataTransfer transfer && transfer.GetHashCode() == GetHashCode();
        public override int GetHashCode() => BitConverter.ToInt32(hash);
        public override string? ToString() => GetHashCode().ToString("x");

        public static bool operator ==(DataTransfer left, DataTransfer right) => left.Equals(right);
        public static bool operator !=(DataTransfer left, DataTransfer right) => !(left == right);
    }
}