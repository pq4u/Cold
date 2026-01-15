import { Routes } from '@angular/router';
import { PackageListComponent } from './package-list/package-list';
import { PackageRentalListComponent } from './package-rental-list/package-rental-list';

export const PACKAGES_ROUTES: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { path: 'list', component: PackageListComponent },
  { path: 'rentals', component: PackageRentalListComponent }
];
