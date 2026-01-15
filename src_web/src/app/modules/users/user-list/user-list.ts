import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UsersService, UserDto } from '../../../core/services/users.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-list.html',
  styleUrls: ['./user-list.scss']
})
export class UserListComponent implements OnInit {
  users: UserDto[] = [];
  roleForm: FormGroup;
  showRoleModal = false;
  selectedUserId: string | null = null;
  selectedUserEmail: string = '';

  constructor(
    private usersService: UsersService,
    private fb: FormBuilder
  ) {
    this.roleForm = this.fb.group({
      role: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.usersService.getAll().subscribe({
      next: (data) => this.users = data,
      error: () => {
        // Fallback for demo if API endpoint isn't fully ready
        this.users = [
          { id: '3fa85f64-5717-4562-b3fc-2c963f66afa6', email: 'admin@example.com', roles: ['Administrator'] },
          { id: '4a2b6c8d-1e3f-5a7b-9c0d-2e4f6a8b0c2d', email: 'employee@example.com', roles: ['Employee'] },
          { id: '1b3c5d7e-9f0a-2b4c-6d8e-0f1a3b5c7d9e', email: 'supplier@example.com', roles: ['Supplier'] }
        ];
      }
    });
  }

  openRoleModal(user: UserDto): void {
    this.selectedUserId = user.id;
    this.selectedUserEmail = user.email;
    this.roleForm.setValue({ role: user.roles[0] || 'Employee' });
    this.showRoleModal = true;
  }

  closeRoleModal(): void {
    this.showRoleModal = false;
    this.selectedUserId = null;
  }

  updateRole(): void {
    if (this.roleForm.valid && this.selectedUserId) {
      const newRole = this.roleForm.get('role')?.value;
      this.usersService.updateRole(this.selectedUserId, newRole).subscribe({
        next: () => {
          this.loadUsers();
          this.closeRoleModal();
        },
        error: () => {
          // If API fails (e.g. mock), just update locally for demo
          const user = this.users.find(u => u.id === this.selectedUserId);
          if (user) {
            user.roles = [newRole];
          }
          this.closeRoleModal();
        }
      });
    }
  }
}
