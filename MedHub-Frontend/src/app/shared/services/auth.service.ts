import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';
import {UserDto, UserService} from "./swagger";
import {TokenService} from "./token.service";

const defaultPath = '/home';

@Injectable()
export class AuthService {
  private _user: UserDto | null = null;

  constructor(
    private router: Router,
    private userService: UserService,
    private tokenService: TokenService
  ) {
  }

  get loggedIn(): boolean {
    return !!this._user;
  }

  // todo: change default path when logging in, patient->Tests
  private _lastAuthenticatedPath: string = defaultPath;

  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
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


  setUser(userDto: UserDto) {
    this._user = userDto;
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
