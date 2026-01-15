import { Routes } from '@angular/router';
import { MainLayoutComponent } from './shared/components/main-layout/main-layout';
import { LoginComponent } from './modules/auth/login/login';
import { RegisterComponent } from './modules/auth/register/register';
import { AuthGuard } from './core/auth/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'catalog', pathMatch: 'full' },
      {
        path: 'catalog',
        loadChildren: () => import('./modules/catalog/catalog.routes').then(m => m.CATALOG_ROUTES)
      },
      {
        path: 'deliveries',
        loadChildren: () => import('./modules/deliveries/deliveries.routes').then(m => m.DELIVERIES_ROUTES)
      },
      {
        path: 'packages',
        loadChildren: () => import('./modules/packages/packages.routes').then(m => m.PACKAGES_ROUTES)
      },
      {
        path: 'users',
        loadChildren: () => import('./modules/users/users.routes').then(m => m.USERS_ROUTES)
      }
    ]
  },
  { path: '**', redirectTo: 'login' }
];