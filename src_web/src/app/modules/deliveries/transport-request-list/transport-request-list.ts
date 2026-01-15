import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TransportRequestsService } from '../../../core/services/transport-requests.service';
import { TransportRequestDto, TransportStatusDto } from '../../../core/models/api-models';

@Component({
  selector: 'app-transport-request-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './transport-request-list.html',
  styleUrls: ['./transport-request-list.scss']
})
export class TransportRequestListComponent implements OnInit {
  requests: TransportRequestDto[] = [];
  statuses: TransportStatusDto[] = [];
  filterForm: FormGroup;
  
  isEditingStatus = false;
  currentRequestId: string | null = null;
  statusForm: FormGroup;

  constructor(
    private transportService: TransportRequestsService,
    private fb: FormBuilder
  ) {
    this.filterForm = this.fb.group({
      statusId: ['']
    });
    
    this.statusForm = this.fb.group({
      transportStatusId: [''],
      actualPickupDate: [''],
      actualDeliveryDate: ['']
    });
  }

  ngOnInit(): void {
    this.loadStatuses();
    this.loadRequests();
    
    this.filterForm.get('statusId')?.valueChanges.subscribe(val => {
       if (val) {
         this.transportService.getByStatus(val).subscribe(data => this.requests = data);
       } else {
         this.loadRequests();
       }
    });
  }

  loadStatuses(): void {
    this.transportService.getStatuses().subscribe(data => this.statuses = data);
  }

  loadRequests(): void {
    this.transportService.getAll().subscribe(data => this.requests = data);
  }

  onUpdateStatus(request: TransportRequestDto): void {
    this.currentRequestId = request.id || null;
    this.isEditingStatus = true;
    this.statusForm.patchValue({
      transportStatusId: request.transportStatusId,
      actualPickupDate: request.actualPickupDate,
      actualDeliveryDate: request.actualDeliveryDate
    });
  }

  cancelEdit(): void {
    this.isEditingStatus = false;
    this.currentRequestId = null;
  }

  submitStatus(): void {
    if (this.currentRequestId && this.statusForm.valid) {
      this.transportService.updateStatus(this.currentRequestId, this.statusForm.value).subscribe(() => {
        this.loadRequests();
        this.isEditingStatus = false;
      });
    }
  }
}