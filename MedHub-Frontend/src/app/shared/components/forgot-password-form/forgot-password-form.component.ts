import {CommonModule} from '@angular/common';
import {Component, NgModule} from '@angular/core';
import {Router, RouterModule} from '@angular/router';
import {DxFormModule} from 'devextreme-angular/ui/form';
import {DxLoadIndicatorModule} from 'devextreme-angular/ui/load-indicator';
import {AuthenticationService} from "../../services/swagger";
import notify from "devextreme/ui/notify";

const notificationText = 'We\'ve sent a link to reset your password. Check your inbox.';

@Component({
  selector: 'app-forgot-password-form',
  templateUrl: './forgot-password-form.component.html',
  styleUrls: ['./forgot-password-form.component.scss']
})
export class ForgotPasswordFormComponent {
  loading = false;
  formData: any = {};

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
  ) {
  }

  async onSubmit(e: Event) {
    e.preventDefault();

    const {email} = this.formData

    this.authenticationService.forgotPassword(email).subscribe({
      next: result => {
        notify("Email sent successfully!");
        this.router.navigate(['/change-password']);
      },
      error: err => {
        notify("Error sending email")
      }
    })

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
