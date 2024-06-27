import {CommonModule} from '@angular/common';
import {Component, NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {ValidationCallbackData} from 'devextreme-angular/common';
import {DxFormModule} from 'devextreme-angular/ui/form';
import {DxLoadIndicatorModule} from 'devextreme-angular/ui/load-indicator';
import {AuthenticationService, ResetPasswordRequestDto} from "../../services/swagger";
import {NotificationService} from "../../services/notification.service";


@Component({
  selector: 'app-change-password-form',
  templateUrl: './change-password-form.component.html',
  styleUrls: ['./change-password-form.component.scss']
})
export class ChangePasswordFormComponent {
  loading = false;
  formData: any = {};

  constructor(
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
  ) {
  }

  async onSubmit(e: Event) {
    e.preventDefault();

    this.loading = true;

    const resetRequest: ResetPasswordRequestDto = {
      email: this.formData.email,
      password: this.formData.password,
      confirmPassword: this.formData.confirmPassword,
      passwordResetCode: this.formData.passwordResetCode,
    }

    this.authenticationService.resetPassword(resetRequest).subscribe({
      next: (response) => {
        this.loading = false;
        this.notificationService.success("Password changed successfully.");
      },
      error: err => {
        this.notificationService.error("Error updating password reset email:", err);
      }, complete: () => {
        this.loading = false
      }
    })
  }

  confirmPassword = (e: ValidationCallbackData) => {
    return e.value === this.formData.password;
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule
  ],
  declarations: [ChangePasswordFormComponent],
  exports: [ChangePasswordFormComponent]
})
export class CreateAccountFormModule {
}
