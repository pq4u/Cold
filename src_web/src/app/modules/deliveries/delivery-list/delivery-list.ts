import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DeliveriesService } from '../../../core/services/deliveries.service';
import { DeliveryDto } from '../../../core/models/api-models';

import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-delivery-list',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './delivery-list.html',
  styleUrls: ['./delivery-list.scss']
})
export class DeliveryListComponent implements OnInit {
  deliveries: DeliveryDto[] = [];
  filterForm: FormGroup;
  
  constructor(
    private deliveriesService: DeliveriesService,
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.filterForm = this.fb.group({
      status: ['all'] // 'all', 'uninvoiced'
    });
  }

  ngOnInit(): void {
    this.loadDeliveries();
    this.filterForm.get('status')?.valueChanges.subscribe(() => {
      this.loadDeliveries();
    });
  }

  loadDeliveries(): void {
    const status = this.filterForm.get('status')?.value;
    const isSupplier = this.authService.isSupplier();
    const userId = this.authService.getUserId();

    if (isSupplier && userId) {
        if (status === 'uninvoiced') {
            this.deliveriesService.getUninvoicedBySupplier(userId).subscribe(data => this.deliveries = data);
        } else {
            this.deliveriesService.getBySupplier(userId).subscribe(data => this.deliveries = data);
        }
    } else {
        if (status === 'uninvoiced') {
            this.deliveriesService.getUninvoiced().subscribe(data => this.deliveries = data);
        } else {
            this.deliveriesService.getAll().subscribe(data => this.deliveries = data);
        }
    }
  }

  markAsInvoiced(id: string): void {
    if (confirm('Czy na pewno chcesz oznaczyć tę dostawę jako zafakturowaną?')) {
      this.deliveriesService.markInvoiced(id).subscribe(() => {
        this.loadDeliveries();
      });
    }
  }
}