import { Component, OnInit } from '@angular/core';
import { Author } from 'src/app/shared/models/author';
import { BiblParams } from 'src/app/shared/models/biblParams';
import { Book } from 'src/app/shared/models/book';
import { Series } from 'src/app/shared/models/series';
import { BibliothecaService } from 'src/app/shared/services/bibliotheca.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  books: Book[] = [];
  series: Series[] = [];
  authors: Author[] = [];
  params = new BiblParams();

  constructor( private bibliothecaService: BibliothecaService){}
  
  ngOnInit(): void {
    this.getBooks();
    this.getAuthors();
    this.getSeries();
  }

  getBooks(){
    this.bibliothecaService.getBooks(this.params).subscribe({
      next: response => this.books = response.data,
      error: error => console.log(error)
    });
  }

  getSeries(){
    this.bibliothecaService.getSeries().subscribe({
      next: response => this.series = response,
      error: error => console.log(error)
    });
  }

  getAuthors(){
    this.bibliothecaService.getAuthors().subscribe({
      next: response => this.authors = response,
      error: error => console.log(error)
    });
  }

  defaultSelected() {
    this.params.authorId = '';
    this.params.seriesId = '';
    this.getBooks();
  }

  onAuthorSelected(authorId: string){
    this.params.authorId = authorId;
    this.params.seriesId = '';
    this.getBooks();
  }

  onSeriesSelected(seriesId: string){
    this.params.seriesId = seriesId;
    this.params.authorId = '';
    this.getBooks();
  }

}
