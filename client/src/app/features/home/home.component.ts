import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/shared/models/book';
import { BibliothecaService } from 'src/app/shared/services/bibliotheca.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  books: Book[] = [];

  constructor( private bibliothecaService: BibliothecaService){}
  
  ngOnInit(): void {
    this.bibliothecaService.getBooks().subscribe({
      next: response => this.books = response.data,
      error: error => console.log(error),
      
    });
  }

}
