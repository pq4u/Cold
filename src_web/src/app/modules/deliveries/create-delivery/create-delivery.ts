import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { DeliveriesService } from '../../../core/services/deliveries.service';
import { ProductsService } from '../../../core/services/products.service';
import { ProductPricesService } from '../../../core/services/product-prices.service';
import { UsersService, UserDto } from '../../../core/services/users.service';
import { ProductDto, CreateDeliveryDto, ProductPriceDto } from '../../../core/models/api-models';

@Component({
  selector: 'app-create-delivery',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './create-delivery.html',
  styleUrls: ['./create-delivery.scss']
})
export class CreateDeliveryComponent implements OnInit {
  deliveryForm: FormGroup;
  products: ProductDto[] = [];
  allPrices: ProductPriceDto[] = [];
  suppliers: UserDto[] = [];

  constructor(
    private fb: FormBuilder,
    private deliveriesService: DeliveriesService,
    private productsService: ProductsService,
    private pricesService: ProductPricesService,
    private usersService: UsersService,
    private router: Router
  ) {
    this.deliveryForm = this.fb.group({
      deliveryNumber: ['', Validators.required],
      supplierId: ['', Validators.required],
      deliveryDate: [new Date().toISOString().substring(0, 16), Validators.required],
      notes: [''],
      products: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.loadProducts();
    this.loadPrices();
    this.loadSuppliers();
    this.addProduct();
  }

  loadProducts(): void {
    this.productsService.getAll().subscribe(data => this.products = data);
  }

  loadPrices(): void {
    this.pricesService.getAll().subscribe(data => this.allPrices = data);
  }

  loadSuppliers(): void {
    this.usersService.getSuppliers().subscribe({
      next: data => this.suppliers = data,
      error: () => {
      }
    });
  }

  getPricesForProduct(productId: string): ProductPriceDto[] {
    if (!productId) return [];
    const now = new Date();
    return this.allPrices.filter(p => 
      p.productId === productId && 
      (!p.dateTo || new Date(p.dateTo) > now)
    );
  }

  get productForms(): FormArray {
    return this.deliveryForm.get('products') as FormArray;
  }

  addProduct(): void {
    const productGroup = this.fb.group({
      productId: ['', Validators.required],
      classType: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(0.01)]]
    });
    
    // Reset classType when productId changes
    productGroup.get('productId')?.valueChanges.subscribe(() => {
      productGroup.get('classType')?.setValue('');
    });

    this.productForms.push(productGroup);
  }

  removeProduct(index: number): void {
    this.productForms.removeAt(index);
  }

  onSubmit(): void {
    if (this.deliveryForm.valid) {
      const deliveryData: CreateDeliveryDto = this.deliveryForm.value;
      this.deliveriesService.create(deliveryData).subscribe({
        next: () => {
          this.router.navigate(['/deliveries']);
        },
        error: (err) => {
          console.error('Failed to create delivery', err);
        }
      });
    }
  }
}