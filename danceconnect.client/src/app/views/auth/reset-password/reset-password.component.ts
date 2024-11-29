import { Component } from '@angular/core';
import { ResetPassword } from './reset-password';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordService } from './reset-password.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  resetViewModel: ResetPassword = new ResetPassword('', '', '');
  isLoading: boolean = false;
  constructor(private router: Router,
    private route: ActivatedRoute,
    private resetPasswordService: ResetPasswordService) {

  }

  onSubmit() {
    if (this.resetViewModel.password !== this.resetViewModel.confirmPassword) {
      return;
    }

    this.resetPasswordService.resetPassword(this.resetViewModel).subscribe(result => {
      console.log(result);

      this.router.navigate(['/auth/login']);
    });
  }

  onCancel() {
    this.router.navigate(['/home']);
  }
}
