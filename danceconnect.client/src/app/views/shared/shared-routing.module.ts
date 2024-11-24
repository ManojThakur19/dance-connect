import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedComponent } from './shared.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { ContactUsDetailComponent } from './contact-us-detail/contact-us-detail.component';
import { ContactUsListComponent } from './contact-us-list/contact-us-list.component';

const routes: Routes = [
  {
    path: '',
    component: SharedComponent,
    children: [
      {
        path: 'contact-us',
        component: ContactUsComponent
      },
      {
        path: 'contact-us/list',
        component: ContactUsListComponent
      },
      { path: 'contact-us/:id', component: ContactUsDetailComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
