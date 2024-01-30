import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

import { BlogPost } from '../models/blog-post';
import { BlogPostService } from '../sevices/blog-post.service';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css'],
})
export class BlogpostListComponent implements OnInit, OnDestroy {
  blogPost$?: Observable<BlogPost[]>;

  constructor(private blogPostService: BlogPostService) {}

  ngOnInit(): void {
    this.blogPost$ = this.blogPostService.getAllBlogPosts();
  }

  ngOnDestroy(): void {}
}
