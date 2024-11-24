import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SetupRoutingModule } from './setup-routing.module';
import { SetUpComponent } from './set-up.component';
import { UsersComponent } from './users/users.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { InstructorProfileComponent } from './instructor-profile/instructor-profile.component';
import { UsersGridComponent } from './users-grid/users-grid.component';
import { InstructorGridComponent } from './instructor-grid/instructor-grid.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtTokenInterceptor } from '../../common/jwt-token.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HeaderComponent } from '../../layout/header/header.component';
import { FooterComponent } from '../../layout/footer/footer.component';
import { SharedModule } from '../shared/shared.module';
import { InstructorDetailComponent } from './instructor-detail/instructor-detail.component';
import { UserDetailComponent } from './user-detail/user-detail.component';


@NgModule({
  declarations: [
    SetUpComponent,
    UserProfileComponent,
    InstructorProfileComponent,
    UsersGridComponent,
    InstructorGridComponent,
    InstructorDetailComponent,
    UserDetailComponent 
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
    SetupRoutingModule,
    UsersComponent,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtTokenInterceptor,
      multi: true
    }
  ]
})
export class SetupModule { }
