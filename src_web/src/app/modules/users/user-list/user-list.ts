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
      }
    });
  }

  openRoleModal(user: UserDto): void {
    this.selectedUserId = user.id;
    this.selectedUserEmail = user.email;
    this.roleForm.setValue({ role: user.roles[0] || '' });
    this.showRoleModal = true;
  }

  closeRoleModal(): void {
    this.showRoleModal = false;
    this.selectedUserId = null
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
          // jak nie zadziala
          // const user = this.users.find(u => u.id === this.selectedUserId);
          // if (user) {
          //   user.role = newRole;
          // }
          this.closeRoleModal();
        }
      });
    }
  }
}
