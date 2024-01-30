import { Category } from '../../category/models/category.model';

export interface AddBlogPost {
  title: string;
  shortDescription: string;
  content: string;
  featuredImageUrl: string;
  urlHandle: string;
  author: string;
  publisheDate: Date;
  isVisible: boolean;
  categories: string[];
}
