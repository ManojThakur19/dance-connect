import { AsyncPipe, DecimalPipe } from '@angular/common';
import { Component, OnInit, PipeTransform } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { NgbHighlight } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../user-profile/user.service';
import { User, UserResponse } from '../user-profile/user';
import { Router, RouterModule } from '@angular/router';

//function search(text: string, pipe: PipeTransform){
//  return COUNTRIES.filter((country) => {
//    const term = text.toLowerCase();
//    return (
//      country.name.toLowerCase().includes(term) ||
//      pipe.transform(country.area).includes(term) ||
//      pipe.transform(country.population).includes(term)
//    );
//  });
//}

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [ReactiveFormsModule, NgbHighlight, RouterModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css',
  providers: []
})
export class UsersComponent implements OnInit {
  users$: UserResponse[] = [];
  filter = new FormControl('', { nonNullable: true });

  constructor(private _userService : UserService, private router: Router) {
    this._userService.getUsers().subscribe(users => {
      this.users$ = users;
    })
  }

  ngOnInit() {
    this._userService.getUsers().subscribe(users => {
      this.users$ = users;
    })
  }
  
}
