import { Component } from '@angular/core';
import { InstructorResponse } from '../instructor-profile/instructor';
import { ActivatedRoute } from '@angular/router';
import { InstructorService } from '../instructor-profile/instructor.service';

@Component({
  selector: 'app-instr-detail-admin',
  templateUrl: './instr-detail-admin.component.html',
  styleUrl: './instr-detail-admin.component.css'
})
export class InstrDetailAdminComponent {
  instructor: InstructorResponse | null = null;
  instructorId: number = 0;

  closeResult = '';
  message: string = '';

  constructor(
    private route: ActivatedRoute,
    private instructorService: InstructorService,
  ) { }

  ngOnInit(): void {
    const instructorId = this.route.snapshot.paramMap.get('id');
    if (instructorId) {
      this.instructorId = parseInt(instructorId);
      this.getInstructor(parseInt(instructorId));
    }
  }

  getInstructor(id: number) {
    this.instructorService.getInstructorById(id).subscribe({
      next: (data) => {
        this.instructor = data;
      },
      error: (err) => console.error('Error fetching instructor:', err),
    });
  }
  approveProfile() {
    this.instructorService.approveInstructor(this.instructorId).subscribe({
      next: (data) => {
        this.instructor = data;
      },
      error: (err) => console.error('Error fetching user:', err),
    });
  }

  declineProfile() {
    this.instructorService.declineInstructor(this.instructorId).subscribe({
      next: (data) => {
        this.instructor = data;
      },
      error: (err) => console.error('Error fetching user:', err),
    });
  }

  downloadIdentityDocument() {
    this.instructorService.downloadDocs(this.instructorId).subscribe((response: Blob) => {
      const link = document.createElement('a');
      link.href = window.URL.createObjectURL(response);
      link.download = 'UserDocuments.zip';
      link.click();
    });
  }

}
