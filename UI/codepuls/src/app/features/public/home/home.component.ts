import { Component, OnDestroy, OnInit } from "@angular/core";

import { BlogPost } from "./../../blog-post/models/blog-post";
import { BlogPostService } from "../../blog-post/sevices/blog-post.service";
import { Observable } from "rxjs";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit, OnDestroy {
  blogs$?: Observable<BlogPost[]>;
  constructor(private blogPostService: BlogPostService) {}

  ngOnInit(): void {
    this.blogs$ = this.blogPostService.getAllBlogPosts();
  }
  ngOnDestroy(): void {}
}
