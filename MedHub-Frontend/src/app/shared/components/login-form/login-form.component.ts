import {CommonModule} from '@angular/common';
import {Component, NgModule} from '@angular/core';
import {Router, RouterModule} from '@angular/router';
import {DxFormModule} from 'devextreme-angular/ui/form';
import {DxLoadIndicatorModule} from 'devextreme-angular/ui/load-indicator';
import {AuthService} from '../../services';
import {AuthenticationResponse, AuthenticationService, LoginRequest, UserDto, UserService} from '../../services/swagger';
import {TokenService} from "../../services/token.service";
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
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private tokenService: TokenService,
    private notificationService: NotificationService,
  ) {
  }

  onSubmit(e: Event) {
    e.preventDefault();
    this.loading = true;

    const loginRequest: LoginRequest = {
      username: this.formData.username,
      password: this.formData.password,
    };

    this.authenticationService.login(loginRequest).subscribe({
      next: (result: AuthenticationResponse) => this.handleLoginResponse(result),
      error: (error) => this.handleError('Login failed', error),
      complete: () => this.loading = false
    });
  }

  private handleLoginResponse(result: AuthenticationResponse) {
    if (result.hasToResetPassword) {
      this.router.navigate(['/change-default-password', result.userId]);
      this.notificationService.warning('Please change your password');
    } else {
      this.fetchUserAndRedirect(result);
    }
  }

  private fetchUserAndRedirect(response: AuthenticationResponse) {
    this.userService.getUserById(response.userId)
      .subscribe({
        next: (userResult: UserDto) => this.setUserAndRedirect(userResult, response.token),
        error: (error) => this.handleError('Error fetching user data', error)
      });
  }

  private setUserAndRedirect(user: UserDto, token: string) {
    this.authService.setUser(user);
    this.tokenService.token = token;
    this.router.navigate(['/home']);
    this.notificationService.success('Login successful');
  }

  private handleError(message: string, error: any) {
    console.error(message, error);
    this.loading = false;
    this.notificationService.error(message);
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
