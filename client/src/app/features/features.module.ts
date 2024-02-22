import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { BookComponent } from './book/book.component';
import { SharedModule } from '../shared/shared.module';
import { BookDetailsComponent } from './book-details/book-details.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { LibraryComponent } from './library/library.component';



@NgModule({
  declarations: [
    HomeComponent,
    BookComponent,
    BookDetailsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    LibraryComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    HomeComponent
  ]
})
export class FeaturesModule { }