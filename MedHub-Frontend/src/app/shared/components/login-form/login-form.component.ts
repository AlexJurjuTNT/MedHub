import {CommonModule} from '@angular/common';
import {Component, NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {DxFormModule} from 'devextreme-angular/ui/form';
import {DxLoadIndicatorModule} from 'devextreme-angular/ui/load-indicator';
import {AuthService} from '../../services';
import {LoginRequest} from '../../services/swagger';
import {NotificationService} from "../../services/notification.service";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {
  loading = false;

  formData: LoginRequest = {
    username: '',
    password: ''
  };

  constructor(
    private authService: AuthService,
    private notificationService: NotificationService,
  ) {
  }

  onSubmit(e: Event) {
    e.preventDefault();
    this.loading = true;

    this.authService.logIn(this.formData.username, this.formData.password).subscribe({
      next: (result) => {
        if (result.isOk) {
          this.notificationService.success('Login successful');
        } else {
          this.notificationService.error(result.message || 'Login failed');
        }
      },
      error: (error) => {
        console.error('Login error', error);
        this.notificationService.error('An unexpected error occurred');
      },
      complete: () => {
        this.loading = false;
      }
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
  declarations: [LoginFormComponent],
  exports: [LoginFormComponent]
})
export class LoginFormModule {
}
