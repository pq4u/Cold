using Cold.Contracts.Core.Contracts.Repositories;
using Cold.Contracts.Core.DAL;
using Cold.Contracts.Core.DAL.Repositories;
using Cold.Contracts.Core.Generator;
using Cold.Contracts.Core.Services;
using Cold.Shared.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Contracts.Core;

public static class Extensions
{
    public static void AddCoreLayer(this IServiceCollection services)
    {
        services.AddPostgres<ContractsDbContext>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IContractProductRepository, ContractProductRepository>();
        services.AddScoped<IContractAmendmentRepository, ContractAmendmentRepository>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IContractAmendmentService, ContractAmendmentService>();
        services.AddScoped<IContractPdfGenerator, ContractPdfGenerator>();
    }
}