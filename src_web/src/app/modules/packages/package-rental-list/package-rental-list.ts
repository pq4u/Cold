import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PackageRentalsService } from '../../../core/services/package-rentals.service';
import { PackagesService } from '../../../core/services/packages.service';
import { AuthService } from '../../../core/auth/auth.service';
import { PackageRentalDto, PackageDto, CreatePackageRentalRequestDto } from '../../../core/models/api-models';

@Component({
  selector: 'app-package-rental-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './package-rental-list.html'
})
export class PackageRentalListComponent implements OnInit {
  requestedRentals: PackageRentalDto[] = [];
  activeRentals: PackageRentalDto[] = [];
  availablePackages: PackageDto[] = [];
  
  rentalForm: FormGroup;
  showRequestForm = false;
  isSupplier = false;
  currentUserId: string | null = null;

  constructor(
    private rentalsService: PackageRentalsService,
    private packagesService: PackagesService,
    private authService: AuthService,
    private fb: FormBuilder
  ) {
    this.rentalForm = this.fb.group({
      items: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.currentUserId = this.authService.getUserId();
    this.isSupplier = this.authService.isSupplier();
    this.loadData();
  }

  loadData(): void {
    this.rentalsService.getRequested().subscribe(data => {
        if (this.isSupplier && this.currentUserId) {
            this.requestedRentals = data.filter(r => r.supplierId === this.currentUserId);
        } else {
            this.requestedRentals = data;
        }
    });
    this.rentalsService.getActive().subscribe(data => {
        if (this.isSupplier && this.currentUserId) {
             this.activeRentals = data.filter(r => r.supplierId === this.currentUserId);
        } else {
             this.activeRentals = data;
        }
    });
    this.packagesService.getAll().subscribe(data => this.availablePackages = data);
  }

  get items(): FormArray {
    return this.rentalForm.get('items') as FormArray;
  }

  openRequestForm(): void {
    this.items.clear();
    this.addItem();
    this.showRequestForm = true;
  }

  addItem(): void {
    const itemGroup = this.fb.group({
      packageId: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]]
    });
    this.items.push(itemGroup);
  }

  removeItem(index: number): void {
    this.items.removeAt(index);
  }

  submitRequest(): void {
    if (this.rentalForm.valid && this.currentUserId) {
      const requestDto: CreatePackageRentalRequestDto = {
        supplierId: this.currentUserId,
        items: this.rentalForm.value.items
      };

      this.rentalsService.request(requestDto).subscribe(() => {
        this.loadData();
        this.showRequestForm = false;
      });
    }
  }

  // Employee Actions
  approve(id: string): void {
    if (confirm('Zatwierdzić to żądanie wypożyczenia?')) {
      this.rentalsService.approve(id).subscribe(() => this.loadData());
    }
  }

  reject(id: string): void {
    if (confirm('Odrzucić to żądanie wypożyczenia?')) {
      this.rentalsService.reject(id).subscribe(() => this.loadData());
    }
  }

  // Employee (or Supplier?) Action - usually Employee registers return
  registerReturn(id: string): void {
    if (confirm('Zarejestrować zwrot dla tego wypożyczenia?')) {
      this.rentalsService.return(id).subscribe(() => this.loadData());
    }
  }

  getPackageName(id: string | undefined): string {
    return this.availablePackages.find(p => p.id === id)?.name || 'Unknown';
  }
}