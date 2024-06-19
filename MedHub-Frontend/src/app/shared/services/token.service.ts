import {Injectable} from "@angular/core";
import {JwtHelperService} from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})

export class TokenService {
  get token() {
    return localStorage.getItem('token') as string;
  }

  set token(token: string) {
    localStorage.setItem('token', token);
  }

  removeToken() {
    return localStorage.removeItem('token');
  }

  isTokenValid() {
    const token = this.token;
    // check if token doesnt exist
    if (!token) {
      return false;
    }

    const jwtHelper = new JwtHelperService();

    // check expiry date
    const isTokenExpired = jwtHelper.isTokenExpired(token);
    if (isTokenExpired) {
      localStorage.clear();
      return false;
    }
    return true;
  }

  isTokenNotValid() {
    return !this.isTokenValid();
  }

  public getUserRole(): string {
    const token = this.token;
    if (token) {
      const jwtHelper = new JwtHelperService();
      const decodedToken = jwtHelper.decodeToken(token);
      return decodedToken.role;
    }
    return "";
  }

  public getUsername(): string {
    const token = this.token;
    if (token) {
      const jwtHelper = new JwtHelperService();
      const decodedToken = jwtHelper.decodeToken(token);
      return decodedToken.sub;

    }
    throw new Error("Username not found");
  }

  public getUserId(): number {
    const token = this.token;
    if (token) {
      const jwtHelper = new JwtHelperService();
      const decodedToken = jwtHelper.decodeToken(token);
      return Number(decodedToken.UserId);

    }
    throw new Error("UserId not found");
  }

}

