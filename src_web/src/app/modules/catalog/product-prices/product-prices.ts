import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductPricesService } from '../../../core/services/product-prices.service';
import { ProductsService } from '../../../core/services/products.service';
import { ProductPriceDto, ProductDto } from '../../../core/models/api-models';

@Component({
  selector: 'app-product-prices',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-prices.html',
  styleUrls: ['./product-prices.scss']
})
export class ProductPricesComponent implements OnInit {
  prices: ProductPriceDto[] = [];
  products: ProductDto[] = [];
  filterForm: FormGroup;
  addPriceForm: FormGroup;
  showForm = false;

  constructor(
    private pricesService: ProductPricesService,
    private productsService: ProductsService,
    private fb: FormBuilder
  ) {
    this.filterForm = this.fb.group({
      productId: ['']
    });

    this.addPriceForm = this.fb.group({
      productId: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]],
      classType: [''],
      dateFrom: [new Date().toISOString().substring(0, 16), Validators.required],
      dateTo: ['']
    });
  }

  ngOnInit(): void {
    this.loadProducts();
    this.loadPrices();

    this.filterForm.get('productId')?.valueChanges.subscribe(val => {
      this.loadPrices(val);
    });
  }

  loadProducts(): void {
    this.productsService.getAll().subscribe(data => this.products = data);
  }

  loadPrices(productId?: string): void {
    if (productId) {
      this.pricesService.get(productId).subscribe(data => this.prices = data);
    } else {
      this.pricesService.getAll().subscribe(data => this.prices = data);
    }
  }

  getProductName(productId: string | undefined): string {
    return this.products.find(p => p.id === productId)?.name || 'Unknown';
  }

  openAddForm(): void {
    this.addPriceForm.reset({
      price: 0,
      dateFrom: new Date().toISOString().substring(0, 16)
    });
    // Pre-select product if filter is active
    const currentFilter = this.filterForm.get('productId')?.value;
    if (currentFilter) {
      this.addPriceForm.patchValue({ productId: currentFilter });
    }
    this.showForm = true;
  }

  closeAddForm(): void {
    this.showForm = false;
  }

  onSubmit(): void {
    if (this.addPriceForm.valid) {
      const newPrice: ProductPriceDto = this.addPriceForm.value;
      this.pricesService.add(newPrice).subscribe(() => {
        this.loadPrices(this.filterForm.get('productId')?.value);
        this.showForm = false;
      });
    }
  }
}
