import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

import { BlogPost } from '../models/blog-post';
import { BlogPostService } from '../sevices/blog-post.service';
import { Category } from '../../category/models/category.model';
import { CategoryService } from '../../category/services/category.service';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';
import { UpdateBlogPostModel } from '../models/update-blog-post-model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css'],
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  routeSubscription?: Subscription;
  updatedSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  deleteBlogPostSubscription?: Subscription;
  imageSelectSubscription?: Subscription;

  model?: BlogPost;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];

  isImageSelectorVisible: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private categoryService: CategoryService,
    private router: Router,
    private imageService: ImageService
  ) {}

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id') ?? '';
        this.getBlogPostSubscription = this.blogPostService
          .getBlogPostById(this.id)
          .subscribe({
            next: (response) => {
              this.model = response;
              this.selectedCategories = response.categories.map((x) => x.id);
            },
          });
      },
    });

    this.imageSelectSubscription = this.imageService.onSelectImage().subscribe({
      next: (response) => {
        if (this.model) {
          this.model.featuredImageUrl = response.url;
          this.closeImageSelector();
        }
      },
    });
  }

  onFormSubmit(): void {
    if (!this.id || !this.model) return;

    const modelUpdateBlogPost: UpdateBlogPostModel = {
      title: this.model.title,
      shortDescription: this.model.title,
      content: this.model.content,
      featuredImageUrl: this.model.featuredImageUrl,
      urlHandle: this.model.urlHandle,
      author: this.model.author,
      publisheDate: this.model.publisheDate,
      isVisible: this.model.isVisible,
      categories: this.selectedCategories ?? [],
    };

    console.log(modelUpdateBlogPost);

    this.updatedSubscription = this.blogPostService
      .updateBlogPost(this.id, modelUpdateBlogPost)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts');
        },
        error: (err: any) => {
          console.error('Error:', err);
        },
      });
  }

  onDelete(): void {
    if (this.id) {
      this.deleteBlogPostSubscription = this.blogPostService
        .deleteBlogpost(this.id)
        .subscribe({
          next: (response) => {
            console.log('Delete by success');
            this.router.navigateByUrl('/admin/blogposts');
          },
          error: (err) => {
            console.error('Error Name:', err.name);
            console.warn('Error Message:', err.message);
            this.router.navigateByUrl('/admin/blogposts');
          },
        });
    }
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = !this.isImageSelectorVisible;
  }

  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updatedSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
  }
}
