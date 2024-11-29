import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { NgbDropdownModule, NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginService } from '../../views/auth/login/login.service';
import { LoginResponse } from '../../views/auth/login/login';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  isAuthenticated?: boolean;
  isAdmin?: boolean;
  user: LoginResponse | null = null;
  returnUrl: string = '';
  constructor(private loginService: LoginService,
    private cdr: ChangeDetectorRef,
    public router: Router, public route: ActivatedRoute) {
    this.loginService.userLoggedIn$.subscribe(user => {
      this.user = user;
    });
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
    var currentUser = localStorage.getItem('currentUser');
    if (currentUser) {
      var parsedUser = JSON.parse(currentUser) as LoginResponse;
      console.log('Stored User', parsedUser);

      //Simply Verifying the token, NOT Evaluating the expiry of token
      if (parsedUser && parsedUser.token) {
        this.user = parsedUser;
        this.isAuthenticated = true;
        this.isAdmin = parsedUser.isAdmin;
        this.cdr.detectChanges();
      }
    }
  }

  viewProfile() {
    const role = this.user?.role;

    if (role == "User") {
      this.router.navigate(['/set-up/user', this.user?.id]);
    } else if (role == "Instructor") {
      this.router.navigate(['/set-up/instructor', this.user?.id]);
    }
    else {
      //See no profile
    }
  }

  logout(): void {
    this.loginService.logOut();
    this.router.navigate([this.returnUrl]);
  }
}
