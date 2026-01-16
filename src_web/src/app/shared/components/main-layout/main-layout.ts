import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.html',
  styleUrls: ['./main-layout.scss'],
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive]
})
export class MainLayoutComponent {
  constructor(public authService: AuthService, private router: Router) {}

  get showCatalog(): boolean {
    return this.authService.hasRole(['Admin', 'Administrator', 'Employee']);
  }

  get showPackages(): boolean {
    return this.authService.hasRole(['Admin', 'Administrator', 'Employee']);
  }

  get showUsers(): boolean {
    return this.authService.hasRole(['Admin', 'Administrator']);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}