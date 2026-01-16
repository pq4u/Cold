import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PackagesService } from '../../../core/services/packages.service';
import { PackageDto } from '../../../core/models/api-models';

@Component({
  selector: 'app-package-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './package-list.html'
})
export class PackageListComponent implements OnInit {
  packages: PackageDto[] = [];
  packageForm: FormGroup;
  isEditing = false;
  currentPackageId: string | null = null;
  showForm = false;

  constructor(
    private packagesService: PackagesService,
    private fb: FormBuilder
  ) {
    this.packageForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      quantity: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.loadPackages();
  }

  loadPackages(): void {
    this.packagesService.getAll().subscribe(data => this.packages = data);
  }

  onAdd(): void {
    this.isEditing = false;
    this.currentPackageId = null;
    this.packageForm.reset({ quantity: 0 });
    this.showForm = true;
  }

  onEdit(pkg: PackageDto): void {
    this.isEditing = true;
    this.currentPackageId = pkg.id || null;
    this.packageForm.patchValue(pkg);
    this.showForm = true;
  }

  onDelete(id: string): void {
    if (confirm('Czy na pewno chcesz usunąć to opakowanie?')) {
      this.packagesService.delete(id).subscribe(() => this.loadPackages());
    }
  }

  onSubmit(): void {
    if (this.packageForm.valid) {
      const pkg: PackageDto = this.packageForm.value;
      
      if (this.isEditing && this.currentPackageId) {
        pkg.id = this.currentPackageId;
        this.packagesService.update(this.currentPackageId, pkg).subscribe(() => {
          this.loadPackages();
          this.showForm = false;
        });
      } else {
        this.packagesService.create(pkg).subscribe(() => {
          this.loadPackages();
          this.showForm = false;
        });
      }
    }
  }

  cancel(): void {
    this.showForm = false;
  }
}