import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { UrlHelper } from '../../../common/url-helper';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) {
  }

  register(data: any): Observable<any> {
    return this.http.post<string>(`${UrlHelper.backEndUrl}/api/account/registration`, data)
      .pipe(map(res => res as any));
  }
}
