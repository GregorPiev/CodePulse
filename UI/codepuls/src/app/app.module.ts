import { AddBlogpostComponent } from "./features/blog-post/add-blogpost/add-blogpost.component";
import { AddCategoryComponent } from "./features/category/add-category/add-category.component";
import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { BlogpostListComponent } from "./features/blog-post/blogpost-list/blogpost-list.component";
import { BrowserModule } from "@angular/platform-browser";
import { CategoryListComponent } from "./features/category/category-list/category-list.component";
import { EditBlogpostComponent } from "./features/blog-post/edit-blogpost/edit-blogpost.component";
import { EditCategoryComponent } from "./features/category/edit-category/edit-category.component";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { NavbarComponent } from "./core/components/navbar/navbar.component";
import { NgModule } from "@angular/core";
import { ImageSelectorComponent } from "./shared/components/image-selector/image-selector.component";
import { HomeComponent } from "./features/public/home/home.component";
import { BlogDetailsComponent } from "./features/public/blog-details/blog-details.component";
import { MarkdownModule } from "ngx-markdown";
import { LoginComponent } from './features/auth/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    BlogpostListComponent,
    AddBlogpostComponent,
    EditBlogpostComponent,
    ImageSelectorComponent,
    HomeComponent,
    BlogDetailsComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MarkdownModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
