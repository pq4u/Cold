namespace Cold.Deliveries.Core.Entities;

internal sealed class TransportStatus
{
    public short Id { get; private set; }
    public string Name { get; private set; }
    public string DisplayName { get; private set; }

    public ICollection<TransportRequest> TransportRequests { get; private set; }

    public TransportStatus(short id, string name, string displayName)
    {
        Id = id;
        Name = name;
        DisplayName = displayName;
        TransportRequests = new HashSet<TransportRequest>();
    }

    private TransportStatus()
    {
    }

    public static class Statuses
    {
        public const short ToRealize = 1;
        public const short OnWayToSupplier = 2;
        public const short AtSupplier = 3;
        public const short OnWayToColdStorage = 4;
        public const short InColdStorage = 5;
        public const short Cancelled = 6;
    }
}
