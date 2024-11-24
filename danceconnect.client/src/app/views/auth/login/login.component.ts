import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Login } from './login';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from './login.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  formData = new Login();
  loginFailedMsg: string = "";
  isLoading = false;
  returnUrl: string = '';

  constructor(private _loginService: LoginService, public router: Router, public route: ActivatedRoute) {
     
  }

  ngOnInit() {
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  Login() {
    this.isLoading = true;
    this._loginService.login(this.formData)
      .pipe(first())
      .subscribe(
        data => {
          if (!data) {
            this.loginFailedMsg = "Incorrect username or password!!";
            this.isLoading = false;
          }
          else {
            this.router.navigate([this.returnUrl]);
          }
        },
        error => {
          this.isLoading = false;
        });
  }
}
