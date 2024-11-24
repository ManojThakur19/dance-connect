import { Component } from '@angular/core';
import { Register, UserType } from './register';
import { RegisterService } from './register.service';
import { Router } from '@angular/router';
import { first } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  formData = new Register();

  isPosting: boolean = false;

  constructor(public _registerService: RegisterService, public router: Router) { }

  register() {
    const user: Register = {
      email : this.formData.email,
      password : this.formData.password,
      confirmPassword : this.formData.confirmPassword,
      userType : parseInt(this.formData.userType.toString()),
    }

    this._registerService.register(user)
      .pipe(first())
      .subscribe(data => {

        this.router.navigate(['/auth/login']);
      },
        error => {
          this.isPosting = false;
        });
  }
}
