import { Routes } from '@angular/router';
import { CategoryListComponent } from './category-list/category-list';
import { ProductListComponent } from './product-list/product-list';
import { ProductPricesComponent } from './product-prices/product-prices';
import { RoleGuard } from '../../core/auth/role.guard';

export const CATALOG_ROUTES: Routes = [
  { path: '', redirectTo: 'categories', pathMatch: 'full' },
  { 
    path: 'categories', 
    component: CategoryListComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin', 'Administrator', 'Employee'] }
  },
  { 
    path: 'products', 
    component: ProductListComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin', 'Administrator', 'Employee'] }
  },
  { 
    path: 'prices', 
    component: ProductPricesComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin', 'Administrator', 'Employee'] }
  }
];
