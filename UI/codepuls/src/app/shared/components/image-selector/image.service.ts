import { BehaviorSubject, Observable } from 'rxjs';

import { BlogImage } from '../../models/blog-image.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ImageService {
  selectedImage: BehaviorSubject<BlogImage> = new BehaviorSubject<BlogImage>({
    id: '',
    fileName: '',
    title: '',
    fileExtension: '',
    url: '',
  });
  constructor(private http: HttpClient) {}

  getAllImages(): Observable<BlogImage[]> {
    return this.http.get<BlogImage[]>(`${environment.apiBaseUrl}/api/images`);
  }

  uploadImage(
    file: File,
    fileName: string,
    title: string
  ): Observable<BlogImage> {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('fileName', fileName);
    formData.append('title', title);

    return this.http.post<BlogImage>(
      `${environment.apiBaseUrl}/api/images`,
      formData
    );
  }

  selectImage(image: BlogImage): void {
    this.selectedImage.next(image);
  }
  onSelectImage(): Observable<BlogImage> {
    return this.selectedImage.asObservable();
  }
}
