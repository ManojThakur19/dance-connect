import { Component, inject, TemplateRef, ViewChild } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { InstructorResponse } from '../instructor-profile/instructor';
import { ActivatedRoute } from '@angular/router';
import { InstructorService } from '../instructor-profile/instructor.service';
import { Rating } from '../../shared/rating/rating';

@Component({
  selector: 'app-instructor-detail',
  templateUrl: './instructor-detail.component.html',
  styleUrl: './instructor-detail.component.css',
  providers: []
})
export class InstructorDetailComponent {
  instructor: InstructorResponse | null = null;
  instructorId: number = 0;

  @ViewChild('closebutton') closebutton : any;
  closeResult = '';
  message: string = '';

  constructor(
    private route: ActivatedRoute,
    private instructorService: InstructorService,
    private modalService : NgbModal,
  ) { }

  ngOnInit(): void {
    const instructorId = this.route.snapshot.paramMap.get('id');
    if (instructorId) {
      this.instructorId = parseInt(instructorId);
      this.getInstructor(parseInt(instructorId));
    }
  }

  getInstructor(id:number) {
    this.instructorService.getInstructorById(id).subscribe({
      next: (data) => {
        this.instructor = data;
      },
      error: (err) => console.error('Error fetching instructor:', err),
    });
  }

  handleRatingChanged(rating: number) {
    if (this.instructor) {
      let rat: Rating = {
        id: 0,
        RatingValue: rating,
        RatedTo: this.instructor?.id ?? 0
      }
      this.instructorService.rateInstructor(rat).subscribe({
        next: (data) => {
          this.getInstructor(this.instructorId);
        },
        error: (err) => console.error('Error fetching instructor:', err),
      });
    }
  }

  sendEmail() {
    if (this.instructor) {
      let message = {
        message: this.message,
        emailTo: this.instructor?.id ?? 0
      }
      this.instructorService.sendEmail(message).subscribe({
        next: (data) => {
          alert('Email has been sent successfully!');
        },
        error: (err) => console.error('Error fetching instructor:', err),
      });
    }
  }

  open(content: TemplateRef<any>) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
      (result) => {
        this.closeResult = `Closed with: ${result}`;
      },
      (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      },
    );
  }

  private getDismissReason(reason: any): string {
    switch (reason) {
      case ModalDismissReasons.ESC:
        return 'by pressing ESC';
      case ModalDismissReasons.BACKDROP_CLICK:
        return 'by clicking on a backdrop';
      default:
        return `with: ${reason}`;
    }
  }
}
