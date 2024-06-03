import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { BookComponent } from './book/book.component';
import { SharedModule } from '../shared/shared.module';
import { BookDetailsComponent } from './book-details/book-details.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { RouterModule } from '@angular/router';
import { RatingComponent } from './book-details/rating/rating.component';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';

@NgModule({
  declarations: [
    HomeComponent,
    BookComponent,
    BookDetailsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    RatingComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    ButtonModule,
    InputTextModule,
    CardModule,
  ],
  exports: [
    HomeComponent
  ]
})
export class FeaturesModule { }