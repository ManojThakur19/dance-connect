import { Component } from '@angular/core';
import { ContactResponse } from '../contact-us/contact';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { ContactUsService } from '../contact-us/contact-us.service';
import { NgbHighlight } from '@ng-bootstrap/ng-bootstrap';


@Component({
    selector: 'app-contact-us-list',
    standalone: true,
    imports: [ReactiveFormsModule, NgbHighlight],
    templateUrl: './contact-us-list.component.html',
    styleUrl: './contact-us-list.component.css'
})
export class ContactUsListComponent {
    contactMessages: ContactResponse[] = [];
    filter = new FormControl('', { nonNullable: true });
    constructor(private _contactUsService: ContactUsService, private router: Router) { }

    ngOnInit() {
        this._contactUsService.getContactUsMessages().subscribe(messages => {
            this.contactMessages = messages;
            console.log('Contact Messages', messages);
        });
    }
}
