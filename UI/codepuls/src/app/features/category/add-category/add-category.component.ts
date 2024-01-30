import { Component, OnDestroy, OnInit } from '@angular/core';

import { AddCategoryRequest } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css'],
})
export class AddCategoryComponent implements OnInit, OnDestroy {

  model!: AddCategoryRequest;
  private addCategorySubscription?: Subscription;


  constructor(
    private categoryService: CategoryService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.model = {
      name: '',
      urlHandle: '',
    };
  }
  onFormSubmit(): void {
    console.log(this.model);
    this.addCategorySubscription = this.categoryService.addCategory(this.model).subscribe({
      next: (response) => {
        console.log('This was successful');
        this.router.navigateByUrl("/admin/categories");
      },
      error: (err) => {

      }
    });
  }

  ngOnDestroy(): void {
      this.addCategorySubscription?.unsubscribe();
  }
}
