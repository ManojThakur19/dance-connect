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
  filteredInstructors: InstructorResponse[] = [];
  searchTerm: string = '';
  gender: string = '';
  city: string = '';
  province: string = '';

  provinces: string[] = [
    'Alberta',
    'British Columbia',
    'Manitoba',
    'New Brunswick',
    'Newfoundland and Labrador',
    'Nova Scotia',
    'Ontario',
    'Prince Edward Island',
    'Quebec',
    'Saskatchewan',
    'Northwest Territories',
    'Nunavut',
    'Yukon'
  ];
  constructor(private _instructorService: InstructorService) { }

  ngOnInit(): void {
    this._instructorService.getInstructors().subscribe(instrcts => {
      this.instructors = instrcts;
      this.filteredInstructors = instrcts;
      console.log('Instructors', instrcts);
    })
  }

  searchInstructor(): InstructorResponse[] {
    this.filteredInstructors =  this.instructors.filter(item =>
      item.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
    return this.filteredInstructors;
  }

  fetchInstructors() {
    const params: any = {};
    if (this.searchTerm) params.searchTerm = this.searchTerm;
    if (this.gender) params.gender = this.gender;
    if (this.city) params.city = this.city;
    if (this.province) params.province = this.province;

    this._instructorService.getInstructors(params).subscribe(instrcts => {
      this.filteredInstructors = instrcts;
    })
  }


  applyFilters() {
    this.fetchInstructors();
  }

  clearFilters() {
    this.searchTerm = '';
    this.gender= '';
    this.city= '';
    this.province = '';
    this.filteredInstructors = this.instructors;
  }
}
