export interface UpdateBlogPostModel {
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
