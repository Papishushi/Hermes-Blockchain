namespace Hermes.BuildingBlock.Data
{
    public interface ITransfer
    {
        byte[] Content { get; }
        byte[] ReceiverDirection { get; }
        byte[] SenderDirection { get; }

        bool Equals(object? obj);
        int GetHashCode();
        string? ToString();
    }
}