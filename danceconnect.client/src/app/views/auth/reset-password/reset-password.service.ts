import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResetPassword } from './reset-password';
import { UrlHelper } from '../../../common/url-helper';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService {

  constructor(private http: HttpClient) { }

  resetPassword(resetViewModel: ResetPassword) {
    return this.http.post<boolean>(`${UrlHelper.backEndUrl}/api/auth/reset-password`, resetViewModel);
  }
}
