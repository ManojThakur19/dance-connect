import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrl: './rating.component.css'
})
export class RatingComponent {
  @Input() rating = 0;
  @Output() ratingChanged = new EventEmitter<number>();

  onRateChanged(event: any) {
    this.ratingChanged.emit(event);
  }
}
