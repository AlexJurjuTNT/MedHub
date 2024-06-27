import {CommonModule} from '@angular/common';
import {Component, NgModule} from '@angular/core';
import {Router, RouterModule} from '@angular/router';
import {DxFormModule} from 'devextreme-angular/ui/form';
import {DxLoadIndicatorModule} from 'devextreme-angular/ui/load-indicator';
import {AuthenticationService, ForgotPasswordRequest} from "../../services/swagger";
import {NotificationService} from "../../services/notification.service";


@Component({
  selector: 'app-forgot-password-form',
  templateUrl: './forgot-password-form.component.html',
  styleUrls: ['./forgot-password-form.component.scss']
})
export class ForgotPasswordFormComponent {
  loading = false;

  formData: ForgotPasswordRequest = {
    username: ''
  };

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private notificationService: NotificationService,
  ) {
  }

  async onSubmit(e: Event) {
    e.preventDefault();

    const forgotPasswordRequest: ForgotPasswordRequest = {
      username: this.formData.username
    }

    this.loading = true;
    this.authenticationService.forgotPassword(forgotPasswordRequest).subscribe({
      next: () => {
        this.notificationService.success("Reset link sent to email");
        this.router.navigate(['/change-password']);
      },
      error: (err) => {
        console.error('Error sending password reset email:', err);
        this.notificationService.error('Error sending password reset email. Please try again');
      },
      complete: () => this.loading = false
    });
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule
  ],
  declarations: [ForgotPasswordFormComponent],
  exports: [ForgotPasswordFormComponent]
})
export class ResetPasswordFormModule {
}
