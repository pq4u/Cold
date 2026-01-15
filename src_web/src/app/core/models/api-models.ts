export interface AuthResponseDto {
  userId: string | null;
  token: string | null;
  expiresAt: string;
}

export interface LoginRequestDto {
  email: string;
  password: string;
}

export interface RegisterRequestDto {
  email: string;
  password: string;
  role: string;
}

export interface CategoryDto {
  id?: string;
  name: string;
  image: string;
}

export interface ProductDto {
  id?: string;
  name?: string | null;
  image?: string | null;
  categoryId?: string;
}

export interface ProductPriceDto {
  productId?: string;
  price?: number;
  classType?: string | null;
  dateFrom?: string;
  dateTo?: string | null;
}

export interface ContractAmendmentDto {
  id?: string;
  contractId?: string;
  amendmentNumber?: string | null;
  title?: string | null;
  content?: string | null;
  reason?: string | null;
  createdAt?: string;
}

export interface ContractDto {
  id?: string;
  contractNumber?: string | null;
  title?: string | null;
  content?: string | null;
  contractStatusId?: number;
  isAccepted?: boolean;
  startDate?: string;
  endDate?: string | null;
  signedDate?: string | null;
  createdAt?: string;
  updatedAt?: string | null;
  productIds?: string[] | null;
}

export interface CreateDeliveryDto {
  deliveryNumber?: string | null;
  supplierId?: string;
  deliveryDate?: string;
  notes?: string | null;
  products?: CreateDeliveryProductDto[] | null;
}

export interface CreateDeliveryProductDto {
  productId?: string;
  classType?: string | null;
  quantity?: number;
}

export interface CreateTransportRequestDto {
  deliveryId?: string | null;
  supplierId?: string;
  requestDate?: string;
  scheduledPickupDate?: string | null;
  notes?: string | null;
}

export interface DeliveryDto {
  id?: string;
  deliveryNumber?: string | null;
  supplierId?: string;
  deliveryDate?: string;
  totalValue?: number;
  notes?: string | null;
  isInvoiced?: boolean;
  products?: DeliveryProductDto[] | null;
  photos?: DeliveryPhotoDto[] | null;
}

export interface DeliveryPhotoDto {
  id?: string;
  deliveryId?: string;
  filePath?: string | null;
  description?: string | null;
}

export interface DeliveryProductDto {
  id?: string;
  deliveryId?: string;
  productId?: string;
  productName?: string | null;
  classType?: string | null;
  quantity?: number;
  unitPrice?: number;
  totalValue?: number;
}

export interface LinkTransportRequestToDeliveryDto {
  deliveryId?: string;
}

export interface TransportRequestDto {
  id?: string;
  deliveryId?: string | null;
  deliveryNumber?: string | null;
  supplierId?: string;
  transportStatusId?: number;
  transportStatusName?: string | null;
  requestDate?: string;
  scheduledPickupDate?: string | null;
  actualPickupDate?: string | null;
  actualDeliveryDate?: string | null;
  notes?: string | null;
}

export interface TransportStatusDto {
  id?: number;
  name?: string | null;
  displayName?: string | null;
}

export interface UpdateTransportStatusDto {
  transportStatusId?: number;
  actualPickupDate?: string | null;
  actualDeliveryDate?: string | null;
}

export interface CreatePackageRentalRequestDto {
  supplierId?: string;
  items?: PackageRentalRequestItemDto[] | null;
}

export interface PackageDto {
  id?: string;
  name: string | null;
  description?: string | null;
  quantity?: number;
}

export interface PackageRentalDto {
  id?: string;
  supplierId?: string;
  status?: string | null;
  requestDate?: string;
  approvalDate?: string | null;
  returnDate?: string | null;
  items?: PackageRentalItemDto[] | null;
}

export interface PackageRentalItemDto {
  id?: string;
  packageId?: string;
  packageName?: string | null;
  quantity?: number;
}

export interface PackageRentalRequestItemDto {
  packageId?: string;
  quantity?: number;
}

export interface ProblemDetails {
  type?: string | null;
  title?: string | null;
  status?: number | null;
  detail?: string | null;
  instance?: string | null;
  [key: string]: any;
}
