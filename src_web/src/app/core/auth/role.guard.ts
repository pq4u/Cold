import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, UrlTree } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean | UrlTree {
    const expectedRole = route.data['expectedRole'];
    const currentRole = this.authService.getRole();

    if (this.authService.isAuthenticated() && (currentRole === expectedRole || (expectedRole === 'Administrator' && (currentRole === 'Admin' || currentRole === 'Administrator')))) {
      return true;
    }

    // Redirect to home or error page if unauthorized
    return this.router.createUrlTree(['/']);
  }
}
