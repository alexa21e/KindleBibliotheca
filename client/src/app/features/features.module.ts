import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { BookComponent } from './book/book.component';



@NgModule({
  declarations: [
    HomeComponent,
    BookComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    HomeComponent
  ]
})
export class FeaturesModule { }