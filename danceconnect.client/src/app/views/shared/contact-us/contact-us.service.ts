import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contact, ContactResponse, MessageReply } from './contact';

@Injectable({
  providedIn: 'root'
})
export class ContactUsService {

  private apiUrl = 'https://localhost:7155/api';

  constructor(private http: HttpClient) { }

  getContactUsMessages(): Observable<ContactResponse[]> {
    return this.http.get<ContactResponse[]>(`${this.apiUrl}/ContactUs`);
  }

  getContactUsMessagesById(id: number): Observable<ContactResponse> {
    return this.http.get<ContactResponse>(`${this.apiUrl}/ContactUs/${id}`);
  }

  saveContact(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(`${this.apiUrl}/ContactUs`, contact);
  }

  sendEmail(response: MessageReply): Observable<any> {
    return this.http.post(`${this.apiUrl}/ContactUs/reply`, response );
  }
}
