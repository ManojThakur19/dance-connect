import { Component } from '@angular/core';
import { UserResponse } from '../user-profile/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user-profile/user.service';
import { LoginResponse } from '../../auth/login/login';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrl: './user-detail.component.css'
})
export class UserDetailComponent {
  user: UserResponse | null = null;
  userId: number = 0;
  isAdmin: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) {
    var currentUser = localStorage.getItem('currentUser');
    if (currentUser) {
      var parsedUser = JSON.parse(currentUser) as LoginResponse;

      if (parsedUser) {
        this.isAdmin = parsedUser.isAdmin;
      }
    }
  }

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('id');
    this.userId = userId ? parseInt(userId) : 0;
    if (userId) {
      this.userService.getUserById(parseInt(userId)).subscribe({
        next: (data) => {
          this.user = data;
        },
        error: (err) => console.error('Error fetching user:', err),
      });
    }
  }

  approveProfile() {
    this.userService.approveUser(this.userId).subscribe({
      next: (data) => {
        this.user = data;
      },
      error: (err) => console.error('Error fetching user:', err),
    });
  }

  declineProfile() {
    this.userService.declineUser(this.userId).subscribe({
      next: (data) => {
        this.user = data;
      },
      error: (err) => console.error('Error fetching user:', err),
    });
  }

  downloadIdentityDocument() {
    const userId = this.user?.id;  
    this.userService.downloadDocs(this.userId).subscribe((response: Blob) => {
      //const contentDisposition = response.get('Content-Disposition') || '';
      //const filenameMatch = contentDisposition.match(/filename="?([^"]+)"?/);
      //const filename = filenameMatch ? filenameMatch[1] : 'downloaded_file';

      const blob = new Blob([response], { type: response.type });
      const link = document.createElement('a');
      link.href = window.URL.createObjectURL(blob);
      link.download = "Identity-docs";
      link.click();
    });
  }

}
