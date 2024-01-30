import { RouterModule, Routes } from "@angular/router";

import { AddBlogpostComponent } from "./features/blog-post/add-blogpost/add-blogpost.component";
import { AddCategoryComponent } from "./features/category/add-category/add-category.component";
import { BlogDetailsComponent } from "./features/public/blog-details/blog-details.component";
import { BlogpostListComponent } from "./features/blog-post/blogpost-list/blogpost-list.component";
import { CategoryListComponent } from "./features/category/category-list/category-list.component";
import { EditBlogpostComponent } from "./features/blog-post/edit-blogpost/edit-blogpost.component";
import { EditCategoryComponent } from "./features/category/edit-category/edit-category.component";
import { HomeComponent } from "./features/public/home/home.component";
import { LoginComponent } from "./features/auth/login/login.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "login", component: LoginComponent },
  { path: "admin/categories", component: CategoryListComponent },
  { path: "admin/categories/add", component: AddCategoryComponent },
  { path: "admin/categories/edit/:id", component: EditCategoryComponent },
  { path: "admin/blogposts", component: BlogpostListComponent },
  { path: "admin/blogposts/add", component: AddBlogpostComponent },
  { path: "admin/blogposts/edit/:id", component: EditBlogpostComponent },
  { path: "blog/:url", component: BlogDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
