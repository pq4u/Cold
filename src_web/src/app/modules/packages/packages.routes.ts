import { Routes } from '@angular/router';
import { PackageListComponent } from './package-list/package-list';
import { PackageRentalListComponent } from './package-rental-list/package-rental-list';
import { RoleGuard } from '../../core/auth/role.guard';

export const PACKAGES_ROUTES: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { 
    path: 'list', 
    component: PackageListComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin', 'Administrator', 'Employee'] }
  },
  { 
    path: 'rentals', 
    component: PackageRentalListComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin', 'Administrator', 'Employee', 'Supplier'] }
  }
];
