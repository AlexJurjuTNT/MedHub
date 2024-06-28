import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';
import {AuthenticationService, LoginRequest, UserDto, UserService} from "./swagger";
import {TokenService} from "./token.service";
import {catchError, map, Observable, of, switchMap, tap} from "rxjs";

const defaultPath = '/home';

@Injectable()
export class AuthService {
  private _user: UserDto | null = null;

  constructor(
    private router: Router,
    private userService: UserService,
    private authenticationService: AuthenticationService,
    private tokenService: TokenService
  ) {
  }

  get loggedIn(): boolean {
    return !!this._user;
  }

  private _lastAuthenticatedPath: string = defaultPath;

  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  public updateUserToken() {
    if (this.tokenService.isTokenValid()) {
      this.userService.getUserById(this.tokenService.getUserId()).subscribe({
        next: (result) => {
          this._user = result;
          this.router.navigate([this._lastAuthenticatedPath]);
        }
      })
    }
  }

  setUser(userDto: UserDto) {
    this._user = userDto;
  }


  logIn(username: string, password: string): Observable<{ isOk: boolean; data?: UserDto; message?: string }> {
    const loginRequest: LoginRequest = {username, password};

    return this.authenticationService.login(loginRequest).pipe(
      switchMap(authResponse => {
        this.tokenService.token = authResponse.token;
        return this.userService.getUserById(authResponse.userId);
      }),
      tap(user => {
        this._user = user;
        this.router.navigate([this._lastAuthenticatedPath]);
      }),
      map(user => ({isOk: true, data: user})),
      catchError(error => {
        console.error('Authentication failed', error);
        return of({isOk: false, message: "Authentication failed"});
      })
    );
  }

  getUser(): Observable<{ isOk: boolean; data: UserDto | null }> {
    return of({
      isOk: !!this._user,
      data: this._user
    });
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
  constructor(
    private router: Router,
    private authService: AuthService
  ) {
  }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn = this.authService.loggedIn;
    const isAuthForm = [
      'login-form',
      'reset-password',
      'change-password',
      'change-default-password'
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
