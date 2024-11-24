import { Component } from '@angular/core';
import { ContactUsService } from '../shared/contact-us/contact-us.service';
import { Contact } from '../shared/contact-us/contact';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  contact: Contact = { name: '', email: '', message: '' };
  constructor(private contactUsService: ContactUsService) { }

  onSubmit(contactForm: any) {
    if (contactForm.valid) {
      this.contactUsService.saveContact(this.contact).subscribe(
        (response) => {
          alert('Contact submitted successfully!');
          contactForm.reset();
        },
        (error) => {
          console.error(error);
          alert('There was an error submitting the contact form.');
        }
      );
    }
  }
}
