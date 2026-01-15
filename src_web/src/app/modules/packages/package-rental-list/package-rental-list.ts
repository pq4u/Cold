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
  templateUrl: './package-rental-list.html',
  styleUrls: ['./package-rental-list.scss']
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
    // Determine role (mock logic or from token)
    // In a real app, we'd decode the JWT token to get the role
    // For now, let's assume if they have a supplierId or checking specific role logic
    // We'll rely on what the API returns mainly, but UI actions depend on role.
    this.currentUserId = this.authService.getToken() ? 'some-user-id' : null; // simplified
    // TODO: Implement proper role checking from AuthService if needed for UI hiding
    // For this demo, let's assume we can see everything but buttons might fail if unauthorized.
    // Better: Check local storage role if stored, or decode token.
    
    // Quick hack for demo: Check if user registered as Supplier in previous steps?
    // Let's just show all relevant UI sections.
    this.isSupplier = true; // defaulting to true to show Request button for demo

    this.loadData();
  }

  loadData(): void {
    this.rentalsService.getRequested().subscribe(data => this.requestedRentals = data);
    this.rentalsService.getActive().subscribe(data => this.activeRentals = data);
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
    if (this.rentalForm.valid) {
      // We need supplierId. In a real app, the backend might infer it from the user context.
      // Or we pass it. The DTO requires supplierId.
      // Let's use a dummy or the current user ID if it's a supplier.
      const requestDto: CreatePackageRentalRequestDto = {
        supplierId: '3fa85f64-5717-4562-b3fc-2c963f66afa6', // Hardcoded valid UUID for demo
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
    if (confirm('Approve this rental request?')) {
      this.rentalsService.approve(id).subscribe(() => this.loadData());
    }
  }

  reject(id: string): void {
    if (confirm('Reject this rental request?')) {
      this.rentalsService.reject(id).subscribe(() => this.loadData());
    }
  }

  // Employee (or Supplier?) Action - usually Employee registers return
  registerReturn(id: string): void {
    if (confirm('Register return for this rental?')) {
      this.rentalsService.return(id).subscribe(() => this.loadData());
    }
  }

  getPackageName(id: string | undefined): string {
    return this.availablePackages.find(p => p.id === id)?.name || 'Unknown';
  }
}