import { Component, OnDestroy, OnInit } from "@angular/core";

import { ActivatedRoute } from "@angular/router";
import { BlogPost } from "../../blog-post/models/blog-post";
import { BlogPostService } from "../../blog-post/sevices/blog-post.service";
import { Observable } from "rxjs";

@Component({
  selector: "app-blog-details",
  templateUrl: "./blog-details.component.html",
  styleUrls: ["./blog-details.component.css"],
})
export class BlogDetailsComponent implements OnInit, OnDestroy {
  url: string | null = null;
  blogPost$?: Observable<BlogPost>;

  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService
  ) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.url = params.get("url");
      },
    });

    if (this.url) {
      this.blogPost$ = this.blogPostService.getBlogPostByURLHandle(this.url);
    }
  }
  ngOnDestroy(): void {}
}
