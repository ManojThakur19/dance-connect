import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from './user';
import { UserService } from './user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent {
  profileForm: FormGroup;
  profilePreview: string | ArrayBuffer | null = null;
  provinces: string[] = ['Ontario', 'Quebec', 'Nova Scotia', 'New Brunswick', 'Manitoba', 'British Columbia', 'Prince Edward Island', 'Saskatchewan', 'Alberta', 'Newfoundland and Labrador'];
  selectedFile: File | null = null;

  constructor(private fb: FormBuilder, private _userService: UserService) {
    this.profileForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      gender: ['', Validators.required],
      dob: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      profilePic: [null, Validators.required],
      identityDocument: [null, Validators.required],
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

  onSubmit() {
    if (this.profileForm.valid) {
      console.log("Form Submitted", this.profileForm.value);
    }

    const user: User = {
      ...this.profileForm.value,
      profilePic: this.selectedFile,
    };

    let formData = new FormData();
    Object.keys(this.profileForm.controls).forEach(key => {
      if (key === 'profilePic' || key === 'identityDocument' || key === 'introVideo') {
        const file = this.profileForm.get(key)?.value;
        if (file) {
          formData.append(key, file);
        }
      } else {
        formData.append(key, this.profileForm.get(key)?.value);
      }
    });

    console.log("Form Data", formData);

    this._userService.addUser(formData).subscribe(
      (response) => {
        console.log('User successfully uploaded:', response);
      },
      (error) => {
        console.error('Error uploading user:', error);
      }
    );
  }

  onCancel() {
    this.profileForm.reset();
    this.profilePreview = null;
  }

}
