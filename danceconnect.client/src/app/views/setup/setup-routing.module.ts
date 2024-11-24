import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SetUpComponent } from './set-up.component';
import { UsersComponent } from './users/users.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UsersGridComponent } from './users-grid/users-grid.component';
import { InstructorGridComponent } from './instructor-grid/instructor-grid.component';
import { InstructorListComponent } from './instructor-list/instructor-list.component';
import { InstructorProfileComponent } from './instructor-profile/instructor-profile.component';
import { InstructorDetailComponent } from './instructor-detail/instructor-detail.component';
import { UserDetailComponent } from './user-detail/user-detail.component';

const routes: Routes = [
  {
    path: '',
    component: SetUpComponent,
    children: [
      {
        path: 'users',
        component: UsersComponent
      },
      { path: 'user/:id', component: UserDetailComponent },
      {
        path: 'user-profile',
        component: UserProfileComponent
      },
      {
        path: 'user-grids',
        component: UsersGridComponent
      },
      {
        path: 'instructors',
        component: InstructorListComponent
      },
      { path: 'instructor/:id', component: InstructorDetailComponent },
      {
        path: 'instructor-profile',
        component: InstructorProfileComponent
      },
      {
        path: 'instructor-grids',
        component: InstructorGridComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SetupRoutingModule { }
