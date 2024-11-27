import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User, UserResponse } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7155/api';
  private endpoint = 'user';

  constructor(private http: HttpClient) { }

  getUsers(): Observable<UserResponse[]> {
    return this.http.get<UserResponse[]>(`${this.apiUrl}/${this.endpoint}`);
  }

  getUserById(id: number): Observable<UserResponse> {
    return this.http.get<UserResponse>(`${this.apiUrl}/${this.endpoint}/${id}`);
  }

  addUser(item: any): Observable<UserResponse> {
    return this.http.post<UserResponse>(`${this.apiUrl}/${this.endpoint}`, item);
  }

  updateUser(id: number, item: any): Observable<UserResponse> {
    return this.http.put<UserResponse>(`${this.apiUrl}/${this.endpoint}/${id}`, item);
  }

  approveUser(id: number): Observable<UserResponse> {
    return this.http.post<UserResponse>(`${this.apiUrl}/${this.endpoint}/approve/${id}`, {});
  }

  declineUser(id: number): Observable<UserResponse> {
    return this.http.post<UserResponse>(`${this.apiUrl}/${this.endpoint}/decline/${id}`, {});
  }

  downloadDocs(id: number) {
    return this.http.get(`${this.apiUrl}/${this.endpoint}/download-docs/${id}`, { responseType: 'blob' })
  }

  deleteUser(id: number): Observable<UserResponse> {
    return this.http.delete<UserResponse>(`${this.apiUrl}/${this.endpoint}/${id}`);
  }
}
