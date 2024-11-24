import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { UrlHelper } from '../../../common/url-helper';
import { AppUser } from '../common/app-user';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) {
  }

  public get currentUserValue(): AppUser {
    return new AppUser();
  }

  login(data: any) {
    return this.http.post<any>(`https://localhost:7155/api/account/login`, data)
      .pipe(map(user => {
        console.log('JUST AFTER LOGGED IN', user);
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
        }
        return user;
      }));
  }
}
