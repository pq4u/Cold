namespace Cold.Contracts.Core.Entities;

internal sealed class ContractStatus
{
    public short Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public ICollection<Contract> Contracts { get; private set; }

    public ContractStatus(short id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        Contracts = new HashSet<Contract>();
    }

    private ContractStatus()
    {
    }
}