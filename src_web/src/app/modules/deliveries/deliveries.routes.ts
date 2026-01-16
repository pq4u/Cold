import { Routes } from '@angular/router';
import { DeliveryListComponent } from './delivery-list/delivery-list';
import { CreateDeliveryComponent } from './create-delivery/create-delivery';
import { RoleGuard } from '../../core/auth/role.guard';

export const DELIVERIES_ROUTES: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { 
    path: 'list', 
    component: DeliveryListComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin', 'Administrator', 'Employee', 'Supplier'] }
  },
  { 
    path: 'create', 
    component: CreateDeliveryComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin', 'Administrator', 'Employee'] }
  }
];
