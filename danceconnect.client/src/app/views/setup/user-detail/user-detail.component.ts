import { Component } from '@angular/core';
import { UserResponse } from '../user-profile/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user-profile/user.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrl: './user-detail.component.css'
})
export class UserDetailComponent {
  instructor: UserResponse | null = null;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('id');
    if (userId) {
      this.userService.getUserById(parseInt(userId)).subscribe({
        next: (data) => {
          this.instructor = data;
        },
        error: (err) => console.error('Error fetching user:', err),
      });
    }
  }
}
