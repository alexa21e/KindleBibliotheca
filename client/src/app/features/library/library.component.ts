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
  });
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

  constructor(private bibliothecaService: BibliothecaService){

  }

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
        // Optionally, you can reset the form here
        this.bookForm.reset();
      },
      error: (error) => {
        console.error('Error creating book:', error);
        // Handle error message/display error to the user
      },
    });
}
}
