import {CommonModule} from '@angular/common';
import {Component, NgModule} from '@angular/core';
import {Router, RouterModule} from '@angular/router';
import {DxFormModule} from 'devextreme-angular/ui/form';
import {DxLoadIndicatorModule} from 'devextreme-angular/ui/load-indicator';
import {AuthService} from '../../services';
import {AuthenticationResponse, AuthenticationService, LoginRequestDto, UserDto, UserService} from "../../services/swagger";
import {TokenService} from "../../services/token.service";


@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {
  loading = false;
  formData: any = {};

  constructor(
    private authService: AuthService,
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private tokenService: TokenService,
  ) {
  }

  onSubmit(e: Event) {
    e.preventDefault();

    this.loading = true;

    const loginRequest: LoginRequestDto = {
      email: this.formData.email,
      password: this.formData.password,
    };

    this.authenticationService.login(loginRequest).subscribe({
      next: (result: AuthenticationResponse) => {

        console.log(result);

        if (result.hasToResetPassword) {
          this.router.navigate(['/change-default-password', result.userId]);
          this.loading = false;
        } else {
          this.userService.getUserById(result.userId).subscribe({
            next: (userResult: UserDto) => {
              this.authService.setUser(userResult);
              this.tokenService.token = result.token;
              this.loading = false;
            },
            error: (error) => {
              console.error('Error fetching user:', error);
              this.loading = false;
            }
          });
        }
      },
      error: (error) => {
        console.error('Error during login:', error);
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
