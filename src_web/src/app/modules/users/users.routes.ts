import { Routes } from '@angular/router';
import { UserListComponent } from './user-list/user-list';
import { UserProfileComponent } from './user-profile/user-profile';
import { RoleGuard } from '../../core/auth/role.guard';

export const USERS_ROUTES: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { 
    path: 'list', 
    component: UserListComponent,
    canActivate: [RoleGuard],
    data: { expectedRole: 'Administrator' }
  },
  { path: 'profile', component: UserProfileComponent }
];
