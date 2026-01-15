import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductsService } from '../../../core/services/products.service';
import { CategoriesService } from '../../../core/services/categories.service';
import { ProductPricesService } from '../../../core/services/product-prices.service';
import { ProductDto, CategoryDto, ProductPriceDto } from '../../../core/models/api-models';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.scss']
})
export class ProductListComponent implements OnInit {
  products: ProductDto[] = [];
  categories: CategoryDto[] = [];
  prices: ProductPriceDto[] = [];
  
  productForm: FormGroup;
  priceForm: FormGroup;
  
  showProductForm = false;
  showPriceModal = false;
  isEditingProduct = false;
  currentProductId: string | null = null;
  selectedProductName: string = '';

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private pricesService: ProductPricesService,
    private fb: FormBuilder
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required]],
      image: [''],
      categoryId: ['', [Validators.required]]
    });

    this.priceForm = this.fb.group({
      price: [0, [Validators.required, Validators.min(0)]],
      classType: [''],
      dateFrom: [new Date().toISOString().substring(0, 16), [Validators.required]],
      dateTo: ['']
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.productsService.getAll().subscribe(data => this.products = data);
    this.categoriesService.getAll().subscribe(data => this.categories = data);
  }

  getCategoryName(id: string | undefined): string {
    return this.categories.find(c => c.id === id)?.name || 'Nieznana';
  }

  onAddProduct(): void {
    this.isEditingProduct = false;
    this.productForm.reset();
    this.showProductForm = true;
  }

  onEditProduct(product: ProductDto): void {
    this.isEditingProduct = true;
    this.currentProductId = product.id || null;
    this.productForm.patchValue({
      name: product.name,
      image: product.image,
      categoryId: product.categoryId
    });
    this.showProductForm = true;
  }

  onDeleteProduct(id: string): void {
    if (confirm('Czy na pewno chcesz usunąć ten produkt?')) {
      this.productsService.remove(id).subscribe(() => this.loadData());
    }
  }

  onSubmitProduct(): void {
    if (this.productForm.valid) {
      const product: ProductDto = this.productForm.value;
      if (this.isEditingProduct && this.currentProductId) {
        product.id = this.currentProductId;
        this.productsService.update(product).subscribe(() => {
          this.loadData();
          this.showProductForm = false;
        });
      } else {
        this.productsService.add(product).subscribe(() => {
          this.loadData();
          this.showProductForm = false;
        });
      }
    }
  }

  onManagePrices(product: ProductDto): void {
    this.currentProductId = product.id || null;
    this.selectedProductName = product.name || '';
    if (this.currentProductId) {
      this.pricesService.get(this.currentProductId).subscribe(data => {
        this.prices = data;
        this.showPriceModal = true;
      });
    }
  }

  onAddPrice(): void {
    if (this.priceForm.valid && this.currentProductId) {
      const price: ProductPriceDto = this.priceForm.value;
      price.productId = this.currentProductId;
      this.pricesService.add(price).subscribe(() => {
        this.pricesService.get(this.currentProductId!).subscribe(data => this.prices = data);
        this.priceForm.reset({
          price: 0,
          dateFrom: new Date().toISOString().substring(0, 16)
        });
      });
    }
  }

  closeModals(): void {
    this.showProductForm = false;
    this.showPriceModal = false;
  }
}