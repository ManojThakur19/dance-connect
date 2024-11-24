import { Component } from '@angular/core';
import { InstructorService } from '../instructor-profile/instructor.service';
import { InstructorResponse } from '../instructor-profile/instructor';

@Component({
  selector: 'app-instructor-grid',
  templateUrl: './instructor-grid.component.html',
  styleUrl: './instructor-grid.component.css'
})
export class InstructorGridComponent {
  instructors: InstructorResponse[] = [];
  searchTerm: string = '';
  constructor(private _instructorService: InstructorService) { }

  ngOnInit(): void {
    this._instructorService.getInstructors().subscribe(instrcts => {
      this.instructors = instrcts;
      console.log('INSTRUCTORS', instrcts);
    })
  }

  get filteredItems(): InstructorResponse[] {
    return this.instructors.filter(item =>
      item.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}
