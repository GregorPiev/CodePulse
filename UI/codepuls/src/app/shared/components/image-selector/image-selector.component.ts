import { Component, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

import { BlogImage } from '../../models/blog-image.model';
import { ImageService } from './image.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css'],
})
export class ImageSelectorComponent implements OnInit, OnDestroy {
  private file?: File;
  fileName?: string;
  title?: string;
  subscriptionUploadImage?: Subscription;
  images$?: Observable<BlogImage[]>;

  @ViewChild('form', { static: false }) imageUploadForm?: NgForm;

  constructor(private imageService: ImageService) {}

  ngOnInit(): void {
    this.getImages();
  }

  onFileUploadChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  }

  uploadImage(): void {
    if (this.file && this.fileName !== '' && this.title !== '') {
      this.subscriptionUploadImage = this.imageService
        .uploadImage(this.file, this.fileName as string, this.title as string)
        .subscribe({
          next: (response) => {
            console.info('Success:', response);
            this.imageUploadForm?.resetForm();
            this.getImages();
          },
          error: (err) => {
            console.log('Error Name:', err.name);
            console.log('Error message: ', err.message);
          },
        });
    }
  }

  private getImages(): void {
    this.images$ = this.imageService.getAllImages();
  }

  selectImage(image: BlogImage): void {
    console.log('Image:', image);
    this.imageService.selectImage(image);
  }

  ngOnDestroy(): void {
    this.subscriptionUploadImage?.unsubscribe();
  }
}
