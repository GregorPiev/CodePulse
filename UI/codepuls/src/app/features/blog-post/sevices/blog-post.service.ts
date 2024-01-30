import { AddBlogPost } from "../models/add-blog-post.model";
import { BlogPost } from "../models/blog-post";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UpdateBlogPostModel } from "../models/update-blog-post-model";
import { environment } from "src/environments/environment.development";

@Injectable({
  providedIn: "root",
})
export class BlogPostService {
  constructor(private http: HttpClient) {}

  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(`${environment.apiBaseUrl}/api/blogpost`);
  }

  getBlogPostById(id: string): Observable<BlogPost> {
    return this.http.get<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost/${id}`
    );
  }

  getBlogPostByURLHandle(urlHandle: string): Observable<BlogPost> {
    return this.http.get<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost/${urlHandle}`
    );
  }

  createBlogPost(data: AddBlogPost): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/blogpost`, data);
  }

  updateBlogPost(
    id: string,
    updatedBlogPost: UpdateBlogPostModel
  ): Observable<BlogPost> {
    return this.http.put<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost/${id}`,
      updatedBlogPost
    );
  }

  deleteBlogpost(id: string): Observable<BlogPost> {
    return this.http.delete<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost/${id}`
    );
  }
}
