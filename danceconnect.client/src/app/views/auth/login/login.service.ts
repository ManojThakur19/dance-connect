import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { UrlHelper } from '../../../common/url-helper';
import { AppUser } from '../common/app-user';
import { LoginResponse } from './login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private loggedInUser = new BehaviorSubject<LoginResponse | null>(null);
  public userLoggedIn$ = this.loggedInUser.asObservable();
  constructor(private http: HttpClient) {
  }

  public get currentUserValue(): AppUser {
    return new AppUser();
  }

  login(data: any) {
    return this.http.post<LoginResponse>(`https://localhost:7155/api/account/login`, data)
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.loggedInUser.next(user);
        }
        return user;
      }));
  }

  logOut(): void {
    this.loggedInUser.next(null);
    localStorage.removeItem('currentUser');
  }
}
