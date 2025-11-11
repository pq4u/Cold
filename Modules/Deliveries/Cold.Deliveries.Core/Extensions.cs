using Cold.Deliveries.Core.Contracts.Repositories;
using Cold.Deliveries.Core.DAL;
using Cold.Deliveries.Core.DAL.Repositories;
using Cold.Deliveries.Core.Services;
using Cold.Shared.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Cold.Deliveries.Core;

public static class Extensions
{
    public static void AddCoreLayer(this IServiceCollection services)
    {
        services.AddPostgres<DeliveriesDbContext>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<IDeliveryProductRepository, DeliveryProductRepository>();
        services.AddScoped<IDeliveryPhotoRepository, DeliveryPhotoRepository>();
        services.AddScoped<ITransportRequestRepository, TransportRequestRepository>();
        services.AddScoped<IDeliveryService, DeliveryService>();
        services.AddScoped<ITransportRequestService, TransportRequestService>();
    }
}
