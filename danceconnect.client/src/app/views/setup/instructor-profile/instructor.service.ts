import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InstructorResponse } from './instructor';
import { Rating } from '../../shared/rating/rating';

@Injectable({
  providedIn: 'root'
})
export class InstructorService {

  private apiUrl = 'https://localhost:7155/api';
  private endpoint = 'instructor';

  constructor(private http: HttpClient) { }

  getInstructors(): Observable<InstructorResponse[]> {
    return this.http.get<InstructorResponse[]>(`${this.apiUrl}/${this.endpoint}`);
  }

  getInstructorById(id: number): Observable<InstructorResponse> {
    return this.http.get<InstructorResponse>(`${this.apiUrl}/${this.endpoint}/${id}`);
  }

  addInstructor(item: any): Observable<InstructorResponse> {
    return this.http.post<InstructorResponse>(`${this.apiUrl}/${this.endpoint}`, item);
  }

  updateInstructor(id: number, item: any): Observable<InstructorResponse> {
    return this.http.put<InstructorResponse>(`${this.apiUrl}/${this.endpoint}/${id}`, item);
  }

  deleteInstructor(id: number): Observable<InstructorResponse> {
    return this.http.delete<InstructorResponse>(`${this.apiUrl}/${this.endpoint}/${id}`);
  }

  rateInstructor(item: Rating): Observable<Rating> {
    return this.http.post<Rating>(`${this.apiUrl}/Ratings`, item);
  }

  sendEmail(item: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/instructor/send-email`, item);
  }

  approveInstructor(id: number): Observable<InstructorResponse> {
    return this.http.post<InstructorResponse>(`${this.apiUrl}/${this.endpoint}/approve/${id}`, {});
  }

  declineInstructor(id: number): Observable<InstructorResponse> {
    return this.http.post<InstructorResponse>(`${this.apiUrl}/${this.endpoint}/decline/${id}`, {});
  }

  downloadDocs(id: number) {
    return this.http.get(`${this.apiUrl}/${this.endpoint}/download-docs/${id}`, { responseType: 'blob' })
  }
}
