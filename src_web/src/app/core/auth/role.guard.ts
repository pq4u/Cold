import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, UrlTree } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean | UrlTree {
    const expectedRoles = route.data['roles'] as string[];
    const currentRole = this.authService.getRole();

    if (this.authService.isAuthenticated() && currentRole) {
      if (expectedRoles && expectedRoles.length > 0) {
        if (expectedRoles.includes(currentRole) || currentRole === 'Admin' || currentRole === 'Administrator') {
           return true;
        }
      } else {
        // If no roles specified, assume authenticated is enough (or deny? usually deny if using RoleGuard)
        // But for this app, RoleGuard implies role check.
        return true;
      }
    }

    return this.router.createUrlTree(['/users/profile']);
  }
}
