import { Routes } from '@angular/router';
import { DeliveryListComponent } from './delivery-list/delivery-list';
import { CreateDeliveryComponent } from './create-delivery/create-delivery';
import { TransportRequestListComponent } from './transport-request-list/transport-request-list';

export const DELIVERIES_ROUTES: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { path: 'list', component: DeliveryListComponent },
  { path: 'create', component: CreateDeliveryComponent },
  { path: 'transport-requests', component: TransportRequestListComponent }
];
