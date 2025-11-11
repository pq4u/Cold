using Cold.Catalog.Shared;
using Cold.Deliveries.Core.Contracts.Repositories;
using Cold.Deliveries.Core.Entities;
using Cold.Deliveries.Shared.Dtos;

namespace Cold.Deliveries.Core.Services;

internal class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly ICatalogModuleApi _catalogModuleApi;

    public DeliveryService(IDeliveryRepository deliveryRepository, ICatalogModuleApi catalogModuleApi)
    {
        _deliveryRepository = deliveryRepository;
        _catalogModuleApi = catalogModuleApi;
    }

    public async Task<DeliveryDto> GetAsync(Guid deliveryId)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        return delivery is null ? null : await MapToDtoAsync(delivery);
    }

    public async Task<DeliveryDto> GetByDeliveryNumberAsync(string deliveryNumber)
    {
        var delivery = await _deliveryRepository.GetByDeliveryNumberAsync(deliveryNumber);
        return delivery is null ? null : await MapToDtoAsync(delivery);
    }

    public async Task<IReadOnlyList<DeliveryDto>> GetAllAsync()
    {
        var deliveries = await _deliveryRepository.GetAllAsync();
        var dtos = new List<DeliveryDto>();

        foreach (var delivery in deliveries)
        {
            dtos.Add(await MapToDtoAsync(delivery));
        }

        return dtos;
    }

    public async Task<IReadOnlyList<DeliveryDto>> GetBySupplierIdAsync(Guid supplierId)
    {
        var deliveries = await _deliveryRepository.GetBySupplierIdAsync(supplierId);
        var dtos = new List<DeliveryDto>();

        foreach (var delivery in deliveries)
        {
            dtos.Add(await MapToDtoAsync(delivery));
        }

        return dtos;
    }

    public async Task<IReadOnlyList<DeliveryDto>> GetUninvoicedAsync()
    {
        var deliveries = await _deliveryRepository.GetUninvoicedAsync();
        var dtos = new List<DeliveryDto>();

        foreach (var delivery in deliveries)
        {
            dtos.Add(await MapToDtoAsync(delivery));
        }

        return dtos;
    }

    public async Task<IReadOnlyList<DeliveryDto>> GetUninvoicedBySupplierIdAsync(Guid supplierId)
    {
        var deliveries = await _deliveryRepository.GetUninvoicedBySupplierIdAsync(supplierId);
        var dtos = new List<DeliveryDto>();

        foreach (var delivery in deliveries)
        {
            dtos.Add(await MapToDtoAsync(delivery));
        }

        return dtos;
    }

    public async Task<Guid> AddAsync(CreateDeliveryDto dto)
    {
        if (await _deliveryRepository.GetByDeliveryNumberAsync(dto.DeliveryNumber) is not null)
        {
            throw new ArgumentException("Delivery number already exists");
        }

        var deliveryId = Guid.NewGuid();
        var delivery = new Delivery(deliveryId, dto.DeliveryNumber, dto.SupplierId, dto.DeliveryDate, 0, dto.Notes);

        decimal totalValue = 0;

        foreach (var productDto in dto.Products)
        {
            var price = await _catalogModuleApi.GetProductPriceAsync(
                productDto.ProductId,
                productDto.ClassType,
                dto.DeliveryDate);

            if (!price.HasValue)
            {
                throw new ArgumentException($"Price not found for product {productDto.ProductId}, class {productDto.ClassType} on date {dto.DeliveryDate:yyyy-MM-dd}");
            }

            var deliveryProduct = new DeliveryProduct(
                Guid.NewGuid(),
                deliveryId,
                productDto.ProductId,
                productDto.ClassType,
                productDto.Quantity,
                price.Value);

            delivery.DeliveryProducts.Add(deliveryProduct);
            totalValue += productDto.Quantity * price.Value;
        }

        delivery.UpdateTotalValue(totalValue);
        await _deliveryRepository.AddAsync(delivery);
        return deliveryId;
    }

    public async Task UpdateAsync(Guid deliveryId, CreateDeliveryDto dto)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery is null)
        {
            throw new ArgumentException("Delivery not found");
        }

        delivery.Update(dto.DeliveryDate, dto.Notes);

        decimal totalValue = 0;
        foreach (var productDto in dto.Products)
        {
            var price = await _catalogModuleApi.GetProductPriceAsync(
                productDto.ProductId,
                productDto.ClassType,
                dto.DeliveryDate);

            if (!price.HasValue)
            {
                throw new ArgumentException(
                    $"Price not found for product {productDto.ProductId}, class {productDto.ClassType} on date {dto.DeliveryDate:yyyy-MM-dd}");
            }

            totalValue += productDto.Quantity * price.Value;
        }

        delivery.UpdateTotalValue(totalValue);

        await _deliveryRepository.UpdateAsync(delivery);
    }

    public async Task DeleteAsync(Guid deliveryId)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery is null)
        {
            throw new ArgumentException("Delivery not found");
        }

        await _deliveryRepository.DeleteAsync(delivery);
    }

    public async Task MarkAsInvoicedAsync(Guid deliveryId)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery is null)
        {
            throw new ArgumentException("Delivery not found");
        }

        delivery.MarkAsInvoiced();
        await _deliveryRepository.UpdateAsync(delivery);
    }

    private async Task<DeliveryDto> MapToDtoAsync(Delivery delivery)
    {
        var productIds = delivery.DeliveryProducts.Select(dp => dp.ProductId).ToList();
        var productNames = await _catalogModuleApi.GetProductsNamesAsync(productIds);
        var productNameDict = productIds.Zip(productNames, (id, name) => new { id, name })
                                       .ToDictionary(x => x.id, x => x.name);

        return new DeliveryDto
        {
            Id = delivery.Id,
            DeliveryNumber = delivery.DeliveryNumber,
            SupplierId = delivery.SupplierId,
            DeliveryDate = delivery.DeliveryDate,
            TotalValue = delivery.TotalValue,
            Notes = delivery.Notes,
            IsInvoiced = delivery.IsInvoiced,
            Products = delivery.DeliveryProducts.Select(dp => new DeliveryProductDto
            {
                Id = dp.Id,
                DeliveryId = dp.DeliveryId,
                ProductId = dp.ProductId,
                ProductName = productNameDict.GetValueOrDefault(dp.ProductId, "Unknown"),
                ClassType = dp.ClassType,
                Quantity = dp.Quantity,
                UnitPrice = dp.UnitPrice,
                TotalValue = dp.TotalValue
            }).ToList(),
            Photos = delivery.Photos?.Select(p => new DeliveryPhotoDto
            {
                Id = p.Id,
                DeliveryId = p.DeliveryId,
                FilePath = p.FilePath,
                Description = p.Description
            }).ToList()
        };
    }
}
