import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { SharedComponent } from './shared.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from '../../layout/header/header.component';
import { FooterComponent } from '../../layout/footer/footer.component';
import { RatingComponent } from './rating/rating.component';
import { NgbHighlight, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtTokenInterceptor } from '../../common/jwt-token.interceptor';
import { ContactUsDetailComponent } from './contact-us-detail/contact-us-detail.component';
import { RouterModule } from '@angular/router';
import { ContactUsListComponent } from './contact-us-list/contact-us-list.component';


@NgModule({
  declarations: [
    SharedComponent,
    ContactUsComponent,
    HeaderComponent,
    FooterComponent,
    RatingComponent,
    ContactUsListComponent,
    ContactUsDetailComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule, ReactiveFormsModule,
    SharedRoutingModule,
    NgbHighlight,
    NgbRatingModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    RatingComponent
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtTokenInterceptor,
      multi: true
    }
  ]
})
export class SharedModule { }
