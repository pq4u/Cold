import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoriesService } from '../../../core/services/categories.service';
import { CategoryDto } from '../../../core/models/api-models';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category-list.html'
})
export class CategoryListComponent implements OnInit {
  categories: CategoryDto[] = [];
  categoryForm: FormGroup;
  isEditing = false;
  currentCategoryId: string | null = null;
  showForm = false;

  constructor(
    private categoriesService: CategoriesService,
    private fb: FormBuilder
  ) {
    this.categoryForm = this.fb.group({
      name: ['', [Validators.required]],
      image: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoriesService.getAll().subscribe(data => {
      this.categories = data;
    });
  }

  onAdd(): void {
    this.isEditing = false;
    this.categoryForm.reset();
    this.showForm = true;
  }

  onEdit(category: CategoryDto): void {
    this.isEditing = true;
    this.currentCategoryId = category.id || null;
    this.categoryForm.patchValue({
      name: category.name,
      image: category.image
    });
    this.showForm = true;
  }

  onDelete(id: string): void {
    if (confirm('Czy na pewno chcesz usunąć tą kategorie?')) {
      this.categoriesService.remove(id).subscribe(() => {
        this.loadCategories();
      });
    }
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const category: CategoryDto = this.categoryForm.value;
      if (this.isEditing && this.currentCategoryId) {
        category.id = this.currentCategoryId;
        this.categoriesService.update(category).subscribe(() => {
          this.loadCategories();
          this.showForm = false;
        });
      } else {
        this.categoriesService.add(category).subscribe(() => {
          this.loadCategories();
          this.showForm = false;
        });
      }
    }
  }

  cancel(): void {
    this.showForm = false;
  }
}