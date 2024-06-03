import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from 'src/app/shared/models/book';
import { Genre } from 'src/app/shared/models/genre';
import { BibliothecaService } from 'src/app/shared/services/bibliotheca.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent implements OnInit {
  book?: Book;
  genreType = Genre;

  selectedFile: File | null = null;
  isUploadCoverDialogVisible: boolean = false;
  isUploadPDFDialogVisible: boolean = false;

  constructor(private bibliothecaService: BibliothecaService,
    private activatedRoute: ActivatedRoute,
    private http: HttpClient,) { }

  ngOnInit(): void {
    this.loadBook();
  }

  loadBook() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.bibliothecaService.getBook(id).subscribe({
        next: response => this.book = response,
        error: error => console.log(error)
      });
    }
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  uploadCover(id: string | undefined) {
    if (!id) {
      console.error('Book ID is undefined');
      return;
    }

    if (!this.selectedFile) {
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.bibliothecaService.uploadCover(id, formData).subscribe(
      {
        next: (response) => {
          this.book!.coverUrl = response.coverUrl;
          this.selectedFile = null;
          this.hideCoverDialog();
        },
        error: (error) => {
          console.error('Error uploading file:', error);
        }
      }
    );
  }

  uploadPDF(id: string | undefined) {
    if (!id) {
      console.error('Book ID is undefined');
      return;
    }

    if (!this.selectedFile) {
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.bibliothecaService.uploadPDF(id, formData).subscribe(
      {
        next: (response) => {
          this.book!.pdfUrl = response.pdfUrl;
          this.selectedFile = null;
          this.hidePDFDialog();
        },
        error: (error) => {
          console.error('Error uploading file:', error);
        }
      }
    );
    this.hidePDFDialog();
    this.loadBook();
  }

  downloadPDF(id: string| undefined ) {
    if (!id) {
      console.error('Book ID is undefined');
      return;
    }
    this.bibliothecaService.downloadPDF(id).subscribe(
      (blob: Blob) => {
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = this.book?.title + '.pdf'; // Give a meaningful file name
        link.click();
        window.URL.revokeObjectURL(url);
      },
      (error) => {
        console.error('Error downloading file:', error);
      }
    );
  }

  showCoverDialog() {
    this.isUploadCoverDialogVisible = true;
  }

  showPDFDialog() {
    this.isUploadPDFDialogVisible = true;
  }

  hideCoverDialog() {
    this.isUploadCoverDialogVisible = false;
  }

  hidePDFDialog() {
    this.isUploadPDFDialogVisible = false;
  }
}
