import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/auth/auth.service';
import { UsersService, UserDto } from '../../../core/services/users.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-profile.html',
  styleUrls: ['./user-profile.scss']
})
export class UserProfileComponent implements OnInit {
  user: UserDto | null = null;
  userId: string | null = null;

  constructor(
    private authService: AuthService,
    private usersService: UsersService
  ) {}

  ngOnInit(): void {
    // In a real app, we might get ID from token or a "me" endpoint
    this.userId = localStorage.getItem('userId');
    
    if (this.userId) {
       // Since we don't have a specific 'getById' in UsersService yet that is confirmed,
       // we might mock this or try to fetch from the list.
       // Ideally: this.usersService.get(this.userId).subscribe(...)
       
       // Mock for demo display based on what we likely have
       this.user = {
         id: this.userId,
         email: 'current.user@example.com', // Placeholder as we don't have full profile endpoint confirmed
         roles: ['Employee'] // Placeholder
       };
    }
  }
}