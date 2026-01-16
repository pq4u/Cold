import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, UrlTree } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) 
  {}

  canActivate(route: ActivatedRouteSnapshot): boolean | UrlTree {
    const expectedRoles = route.data['roles'] as string[];
    const currentRole = this.authService
                                      .getRole();

    if (this.authService.isAuthenticated() && currentRole) {
      if (expectedRoles && expectedRoles.length > 0) {
        if (expectedRoles.includes(currentRole) || currentRole === 'Admin' || currentRole === 'Administrator') {
           return true;
        }
      } else {
        // co jesli brak roli?
        return true;
      }
    }

    return this.router.createUrlTree(['/users/profile']);
  }
}
