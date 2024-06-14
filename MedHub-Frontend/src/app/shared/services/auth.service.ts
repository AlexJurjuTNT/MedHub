import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';
import {AuthenticationResponse, AuthenticationService, LoginRequestDto, UserDto, UserService} from "./swagger";
import {TokenService} from "./token.service";

const defaultPath = '/';

@Injectable()
export class AuthService {
  private _user: UserDto | null = null;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private tokenService: TokenService
  ) {
  }

  // todo: if token is set and valid get user using the token value
  get loggedIn(): boolean {
    return !!this._user;
  }

  public updateUserToken() {
    if (this.tokenService.isTokenValid()) {
      this.userService.getUserById(this.tokenService.getUserId()).subscribe({
        next: (result) => {
          this._user = result;
        }
      })
    }
  }

  private _lastAuthenticatedPath: string = defaultPath;

  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  async logIn(email: string, password: string): Promise<{ isOk: boolean; data?: UserDto; message?: string }> {
    const loginRequest: LoginRequestDto = {
      email: email,
      password: password
    };

    return new Promise((resolve) => {
      this.authenticationService.login(loginRequest).subscribe({
        next: (authResponse: AuthenticationResponse) => {
          this.tokenService.token = authResponse.token;

          this.userService.getUserById(authResponse.userId).subscribe({
            next: (user: UserDto) => {
              this._user = user;
              this.router.navigate([this._lastAuthenticatedPath]);
              resolve({isOk: true, data: this._user});
            },
            error: () => resolve({isOk: false, message: "Failed to retrieve user details"})
          });
        },
        error: () => resolve({isOk: false, message: "Authentication failed"})
      });
    });
  }


  async getUser() {
    try {
      // Send request

      return {
        isOk: true,
        data: this._user
      };
    } catch {
      return {
        isOk: false,
        data: null
      };
    }
  }

  getUserSync() {
    try {
      return {
        isOk: true,
        data: this._user
      }
    } catch {
      return {
        isOk: false,
        data: null
      }
    }
  }

  async createAccount(email: string, password: string) {
    try {
      // Send request

      this.router.navigate(['/create-account']);
      return {
        isOk: true
      };
    } catch {
      return {
        isOk: false,
        message: "Failed to create account"
      };
    }
  }

  async changePassword(email: string, recoveryCode: string) {
    try {
      // Send request

      return {
        isOk: true
      };
    } catch {
      return {
        isOk: false,
        message: "Failed to change password"
      }
    }
  }

  async resetPassword(email: string) {
    try {
      // Send request

      return {
        isOk: true
      };
    } catch {
      return {
        isOk: false,
        message: "Failed to reset password"
      };
    }
  }

  async logOut() {
    this._user = null;
    this.tokenService.removeToken();
    this.router.navigate(['/login-form']);
  }
}

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn = this.authService.loggedIn;
    const isAuthForm = [
      'login-form',
      'reset-password',
      'change-password',
    ].includes(route.routeConfig?.path || defaultPath);

    if (isLoggedIn && isAuthForm) {
      this.authService.lastAuthenticatedPath = defaultPath;
      this.router.navigate([defaultPath]);
      return false;
    }

    if (!isLoggedIn && !isAuthForm) {
      this.router.navigate(['/login-form']);
    }

    if (isLoggedIn) {
      this.authService.lastAuthenticatedPath = route.routeConfig?.path || defaultPath;
    }

    return isLoggedIn || isAuthForm;
  }
}
