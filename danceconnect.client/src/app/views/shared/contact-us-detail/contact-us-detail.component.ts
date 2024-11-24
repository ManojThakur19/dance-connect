import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactResponse, MessageReply } from '../contact-us/contact';
import { ContactUsService } from '../contact-us/contact-us.service';

@Component({
  selector: 'app-contact-us-detail',
  templateUrl: './contact-us-detail.component.html',
  styleUrl: './contact-us-detail.component.css'
})
export class ContactUsDetailComponent {
  contactUs: ContactResponse | null = null;
  contactUsId: number = 0;

  question: string = '';
  response: string = '';
  constructor(
    private contactUsService : ContactUsService,
    private route: ActivatedRoute,
    private router : Router
  ) { }

  ngOnInit(): void {
    const contactUsId = this.route.snapshot.paramMap.get('id');
    if (contactUsId) {
      this.contactUsId = parseInt(contactUsId);
      this.getContactMessageById(parseInt(contactUsId));
    }
  }

  getContactMessageById(id: number) {
    this.contactUsService.getContactUsMessagesById(id).subscribe({
      next: (data) => {
        this.contactUs = data;
        this.question = this.contactUs.message;
        console.log('ContactUsMeesage', this.contactUs);
      },
      error: (err) => console.error('Error fetching instructor:', err),
    });
  }

  sendResponse(): void {
    if (this.response) {
      let messageReply: MessageReply = {
        replyTo: this.contactUsId,
        message: this.response
      }

      this.contactUsService.sendEmail(messageReply).subscribe({
          next: (data) => {
          alert('Email has been sent successfully!');
          this.router.navigate(['/contact-us/list']);
          },
          error: (err) => console.error('Error fetching instructor:', err),
        });
      }
      this.response = '';
    }
  }
