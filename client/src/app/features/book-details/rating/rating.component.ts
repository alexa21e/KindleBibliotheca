import { Component, Input } from '@angular/core';
import { Book } from 'src/app/shared/models/book';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss']
})
export class RatingComponent {
  @Input() book?: Book;
}
