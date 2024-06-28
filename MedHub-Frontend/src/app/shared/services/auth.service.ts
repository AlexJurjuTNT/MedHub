import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';
import {AuthenticationService, LoginRequest, UserDto, UserService} from "./swagger";
import {TokenService} from "./token.service";
import {catchError, map, Observable, of, switchMap, tap} from "rxjs";
import {Role} from "./role.enum";

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
    this._lastAuthenticatedPath = defaultPath;
    this.tokenService.removeToken();
    this.router.navigate(['/login-form']);
  }
}

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(
    private router: Router,
    private tokenService: TokenService,
    private authService: AuthService
  ) {
  }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
    return this.authService.getUser().pipe(
      map(result => {
        const isLoggedIn = result.isOk && result.data !== null;
        const user = result.data;
        const role: Role | null = this.tokenService.getUserRole();

        const isAuthForm = [
          'login-form',
          'reset-password',
          'change-password',
          'change-default-password'
        ].includes(route.routeConfig?.path || defaultPath);

        const patientAllowedPaths = [
          'pages/test-result-view',
          'pages/patient-tests'
        ];

        // If user is logged in and on an auth form, redirect to home
        if (isLoggedIn && isAuthForm) {
          this.authService.lastAuthenticatedPath = defaultPath;
          this.router.navigate([defaultPath]);
          return false;
        }

        // If user is not logged in and not on an auth form, redirect to login
        if (!isLoggedIn && !isAuthForm) {
          this.router.navigate(['/login-form']);
          return false;
        }

        // If user is a patient, check if they're trying to access an allowed path
        if (isLoggedIn && user && role === 'Patient') {
          const currentPath = route.routeConfig?.path || '';
          const isAllowedPath = patientAllowedPaths.some(path => currentPath.startsWith(path));

          if (!isAllowedPath) {
            this.router.navigate(['/pages/patient-tests', user.id]);
            return false;
          }
        }

        if (isLoggedIn) {
          this.authService.lastAuthenticatedPath = route.routeConfig?.path || defaultPath;
        }

        return isLoggedIn || isAuthForm;
      })
    );
  }
}
