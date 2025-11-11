using Cold.Catalog.Shared;
using Cold.Contracts.Core.DAL;
using Cold.Contracts.Core.Enums;

namespace Cold.Contracts.Core.Generator;

internal class ContractDocumentDataSource
{
    private readonly ContractsDbContext _dbContext;
    private readonly ICatalogModuleApi _catalogModuleApi;

    public ContractDocumentDataSource(ContractsDbContext dbContext, ICatalogModuleApi catalogModuleApi)
    {
        _dbContext = dbContext;
        _catalogModuleApi = catalogModuleApi;
    }

    public async Task<ContractModel> GetContractDetailsAsync(Guid contractId)
    {
        var contractDb = _dbContext.Contracts.SingleOrDefault(x => x.Id == contractId);
        var contractProducts = _dbContext.ContractProducts
            .Where(x => x.ContractId == contractId)
            .Select(x => x.ProductId)
            .ToList();
        var status = _dbContext.ContractStatuses.SingleOrDefault(x => x.Id == contractDb!.ContractStatusId)!.Name;
        var productsNames = await _catalogModuleApi.GetProductsNamesAsync(contractProducts);
        
        if (contractDb is null)
        {
            throw new ArgumentException("Contract does not exist");
        }
        
        var contractModel = new ContractModel()
        {
            Content = contractDb.Content,
            CreatedAt = contractDb.CreatedAt,
            EndDate = contractDb.EndDate,
            IsAccepted = contractDb.IsAccepted,
            Number = contractDb.ContractNumber,
            ProductsNames = productsNames,
            SignedDate = contractDb.SignedDate,
            StartDate = contractDb.StartDate,
            Status = status,
            Title = contractDb.Title
        };
        
        return contractModel;
    }
}