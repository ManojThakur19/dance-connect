import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InstructorService } from './instructor.service';
import { Instructor } from './instructor';

@Component({
  selector: 'app-instructor-profile',
  templateUrl: './instructor-profile.component.html',
  styleUrl: './instructor-profile.component.css'
})
export class InstructorProfileComponent {
  

  profileForm: FormGroup;
  profilePreview: string | ArrayBuffer | null = null;
  provinces: string[] = ['Ontario', 'Quebec', 'Nova Scotia', 'New Brunswick', 'Manitoba', 'British Columbia', 'Prince Edward Island', 'Saskatchewan', 'Alberta', 'Newfoundland and Labrador'];
  selectedFile: File | null = null;

  constructor(private fb: FormBuilder, private _instructorService: InstructorService) {
    this.profileForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      gender: ['', Validators.required],
      dob: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      hourlyRate: ['', Validators.required],
      profilePic: [null, Validators.required],
      identityDocument: [null, Validators.required],
      shortIntroVideo: [null, Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
      province: ['', Validators.required]
    });
  }

  onProfilePicChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.profilePreview = reader.result;
        //this.selectedFile = file;
        this.profileForm.get('profilePic')?.setValue(file);
      };
      reader.readAsDataURL(file);
    }
  }

  onIdentityDocChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.profileForm.get('identityDocument')?.setValue(file);
    }
  }

  onShortVideoChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.profileForm.get('shortIntroVideo')?.setValue(file);
    }
  }

  onSubmit() {
    if (this.profileForm.valid) {
      console.log("Form Submitted", this.profileForm.value);
    }

    const user: Instructor = {
      ...this.profileForm.value,
      profilePic: this.selectedFile,
    };

    let formData = new FormData();
    Object.keys(this.profileForm.controls).forEach(key => {
      const controlValue = this.profileForm.get(key)?.value;
      if (controlValue instanceof File || controlValue instanceof Blob) {
        const file = this.profileForm.get(key)?.value;
        if (file) {
          formData.append(key, controlValue);
        }
      } else {
        formData.append(key, controlValue);
      }
    });

    console.log("Form Data", formData);

    this._instructorService.addInstructor(formData).subscribe(
      (response: any) => {
        console.log('Instructor successfully uploaded:', response);
      },
      (error: Error) => {
        console.error('Error uploading user:', error);
      }
    );
  }

  onCancel() {
    this.profileForm.reset();
    this.profilePreview = null;
  }
}
