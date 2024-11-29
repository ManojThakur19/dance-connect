import { Component, OnInit } from '@angular/core';
import { UserResponse } from '../user-profile/user';
import { UserService } from '../user-profile/user.service';

@Component({
  selector: 'app-users-grid',
  templateUrl: './users-grid.component.html',
  styleUrl: './users-grid.component.css'
})

export class UsersGridComponent implements OnInit {

  users: UserResponse[] = [];
  filteredUsers: UserResponse[] = [];
  searchTerm: string = '';
  constructor(private _userService: UserService) { }

  ngOnInit(): void {
    this._userService.getUsers().subscribe(usrs => {
      this.users = usrs;
      this.filteredUsers = usrs;
      console.log('USERS', usrs);
    })
  }

  get filteredItems(): UserResponse[] {
    return this.users.filter(item =>
      item.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  searchUsers() {
    this.filteredUsers = this.users.filter(item =>
      item.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
    return this.filteredUsers;
  }
}
