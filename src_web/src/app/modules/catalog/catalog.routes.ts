import { Routes } from '@angular/router';
import { CategoryListComponent } from './category-list/category-list';
import { ProductListComponent } from './product-list/product-list';
import { ProductPricesComponent } from './product-prices/product-prices';

export const CATALOG_ROUTES: Routes = [
  { path: '', redirectTo: 'categories', pathMatch: 'full' },
  { path: 'categories', component: CategoryListComponent },
  { path: 'products', component: ProductListComponent },
  { path: 'prices', component: ProductPricesComponent }
];
