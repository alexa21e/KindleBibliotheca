import { Component, Input } from '@angular/core';
import { NumberValueAccessor } from '@angular/forms';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent {
@Input() pageIndex?: number;
@Input() pageSize?: number;
@Input() totalCount?: number;
}
