using Cold.Catalog.Shared;
using Cold.Contracts.Core.DAL;
using QuestPDF.Fluent;

namespace Cold.Contracts.Core.Generator;

internal class ContractPdfGenerator : IContractPdfGenerator
{
    private readonly ContractsDbContext _dbContext;
    private readonly ICatalogModuleApi _catalogModuleApi;

    public ContractPdfGenerator(ContractsDbContext dbContext, ICatalogModuleApi catalogModuleApi)
    {
        _dbContext = dbContext;
        _catalogModuleApi = catalogModuleApi;
    }

    public async Task<byte[]> GenerateAsync(Guid contractId)
    {
        var dataSource = new ContractDocumentDataSource(_dbContext, _catalogModuleApi);

        var model = await dataSource.GetContractDetailsAsync(contractId);

        var document = new ContractDocument(model);
        
        //document.GeneratePdfAndShow();
        document.GeneratePdf("C:\\temp\\test.pdf");

        return Array.Empty<byte>();
    }
}