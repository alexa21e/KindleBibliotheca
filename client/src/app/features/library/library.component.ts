import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Book } from 'src/app/shared/models/book';
import { BibliothecaService } from 'src/app/shared/services/bibliotheca.service';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss']
})
export class LibraryComponent {
  book?: Book;
  bookForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    authorName: new FormControl('', [Validators.required]),
    publishingDate: new FormControl('', [Validators.required]),
    rating: new FormControl('', [Validators.required]),
    //genre: new FormControl(''),
    publishingHouse: new FormControl('', [Validators.required]),
    // series: new FormControl(''),
    // seriesPlace: new FormControl(''),
    pagesNumber: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    coverUrl: new FormControl('', [Validators.required]),
  });

  selectedFile: File | null = null;

  constructor(private http: HttpClient,
    private bibliothecaService: BibliothecaService) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  uploadCover() {
    if (!this.selectedFile) {
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.http.post<any>('https://localhost:5001/api/upload/cover', formData).subscribe(
      (response) => {
        // Assuming the server responds with the file path
        const coverUrl = response.coverUrl;
        // Here, you would save the coverUrl to your book entity
        this.bookForm.get('coverUrl')?.setValue(coverUrl);
        //"coverUrl": "https://localhost:5001/Uploads/61306ba4-5aa8-4914-8f67-1c78ef5f899e.jpg"
        //"coverUrl": "https://localhost:5001/images/ahsfab.jpg",
      },
      (error) => {
        console.error('Error uploading file:', error);
      }
    );
  }
  
  // genreOptions = [
  //   { name: 'Fantasy', value: 0 },
  //   { name: 'Fiction', value: 1 },
  //   { name: 'Horror', value: 2 },
  //   { name: 'Non-Fiction', value: 3 },
  //   { name: 'Psychology', value: 4 },
  //   { name: 'Romance', value: 5 },
  //   { name: 'Self Development', value: 6 },
  //   { name: 'Thriller', value: 7 },
  //   { name: 'Young Adult', value: 8 },
  // ];
  visible: boolean = false;
  response!: { coverImagePath: ''; };


  showDialog() {
    this.visible = true;
  }

  uploadFinished = (event: { coverImagePath: ""; }) => { 
    this.response = event; 
  }

  onCreate() {
    this.bibliothecaService.createBook(this.bookForm.value).subscribe({
      next: (response) => {
        console.log('Book created successfully!', response);
        this.bookForm.reset();
      },
      error: (error) => {
        console.error('Error creating book:', error);
      },
    });
}
}
