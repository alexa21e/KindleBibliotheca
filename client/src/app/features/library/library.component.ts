import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Book } from 'src/app/shared/models/book';
import { Genre } from 'src/app/shared/models/genre';
import { BibliothecaService } from 'src/app/shared/services/bibliotheca.service';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss']
})
export class LibraryComponent {
  visible: boolean = false;
  book?: Book;
  bookForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    authorName: new FormControl('', [Validators.required]),
    publishingDate: new FormControl('', [Validators.required]),
    rating: new FormControl('', [Validators.required]),
    genre: new FormControl(''),
    publishingHouse: new FormControl('', [Validators.required]),
    series: new FormControl(''),
    seriesPlace: new FormControl(''),
    pagesNumber: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    coverUrl: new FormControl('', [Validators.required]),
  });
  genreOptions = Object.keys(Genre)
  .filter(key => !isNaN(Number(key)))
  .map(key => ({ value: Genre[key as any], name: Genre[key as any] }));

  constructor(private bibliothecaService: BibliothecaService) { }

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
    this.hideDialog();
  }

  showDialog() {
    this.visible = true;
  }

  hideDialog() {
    this.visible = false;
  }
}

