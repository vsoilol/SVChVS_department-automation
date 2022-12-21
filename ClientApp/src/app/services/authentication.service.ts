import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { AuthenticationResult } from '../models/Response/AuthenticationResult';

import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { User, Role } from '../models/Response/User';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  private url = environment.apiUrl;
  private expireDays: number = 90;

  constructor(
    private router: Router,
    private http: HttpClient,
    private cookieService: CookieService
  ) {
    this.userSubject = new BehaviorSubject<User>(null);
    this.user = this.userSubject.asObservable();
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  login(email: string, password: string) {
    return this.http
      .post<AuthenticationResult>(
        this.url + 'identity/login',
        { email, password },
        { withCredentials: true }
      )
      .pipe(
        map((authenticationResult) => {
          this.setRefreshToken(authenticationResult);

          this.setUserData(authenticationResult);
          this.startRefreshTokenTimer();
          return authenticationResult;
        })
      );
  }

  loginByFullName(
    name: string,
    surname: string,
    patronymic: string,
    password: string
  ) {
    return this.http
      .post<AuthenticationResult>(
        this.url + 'identity/login/fullName',
        { name, surname, patronymic, password },
        { withCredentials: true }
      )
      .pipe(
        map((authenticationResult) => {
          this.setRefreshToken(authenticationResult);

          this.setUserData(authenticationResult);
          this.startRefreshTokenTimer();
          return authenticationResult;
        })
      );
  }

  logout() {
    const refreshToken: string = this.getCookie('refreshToken');

    return this.http
      .post<any>(
        this.url + 'identity/logout',
        { refreshToken },
        {
          withCredentials: true,
        }
      )
      .pipe(
        map((authenticationResult) => {
          this.router.navigate(['']);
          this.stopRefreshTokenTimer();
          this.userSubject.next(null);
          return authenticationResult;
        })
      );
  }

  register(email: string, password: string) {
    return this.http
      .post<AuthenticationResult>(this.url + 'identity/register', {
        email,
        password,
      })
      .pipe(
        map((authenticationResult) => {
          this.setUserData(authenticationResult);
          this.startRefreshTokenTimer();
          return authenticationResult;
        })
      );
  }

  setUserData(authenticationResult: AuthenticationResult): void {
    const helper = new JwtHelperService();

    const decodedToken = helper.decodeToken(authenticationResult.token);

    const typedRoleString = decodedToken.role as keyof typeof Role;
    const role = Role[typedRoleString];

    const user: User = {
      id: decodedToken.id,
      email: decodedToken.email,
      role: role,
      jwtToken: authenticationResult.token,
      firstName: decodedToken.firstName,
      surname: decodedToken.surname,
      patronymic: decodedToken.patronymic,
      departmentId: decodedToken.departmentId
        ? parseInt(decodedToken.departmentId)
        : null,
    };

    this.userSubject.next(user);
  }

  b64_to_utf8(str: string) {
    return decodeURIComponent(escape(window.atob(str)));
  }

  parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(
      this.b64_to_utf8(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join('')
    );

    return JSON.parse(jsonPayload);
  }

  toBinary(string: string) {
    const codeUnits = new Uint16Array(string.length);
    for (let i = 0; i < codeUnits.length; i++) {
      codeUnits[i] = string.charCodeAt(i);
    }
    const charCodes = new Uint8Array(codeUnits.buffer);
    let result = '';
    for (let i = 0; i < charCodes.byteLength; i++) {
      result += String.fromCharCode(charCodes[i]);
    }
    return result;
  }

  activateUser(id: string) {
    return this.http.get<void>(this.url + `identity/activate/${id}`);
  }

  deactivateUser(id: string) {
    return this.http.get<void>(this.url + `identity/deactivate/${id}`);
  }

  refreshToken() {
    const refreshToken: string = this.getCookie('refreshToken');

    return this.http
      .post<AuthenticationResult>(
        this.url + 'identity/refresh',
        { refreshToken },
        {
          withCredentials: true,
        }
      )
      .pipe(
        map((authenticationResult) => {
          this.setRefreshToken(authenticationResult);

          this.setUserData(authenticationResult);
          this.startRefreshTokenTimer();
          return authenticationResult;
        }),
        catchError((ex) => of('error', ex))
      );
  }

  private refreshTokenTimeout: any;

  private setRefreshToken(authenticationResult: AuthenticationResult) {
    this.setCookie(
      'refreshToken',
      authenticationResult.refreshToken,
      this.expireDays
    );
  }

  private setCookie(name, value, days) {
    var expires = '';
    if (days) {
      var date = new Date();
      date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
      expires = '; expires=' + date.toUTCString();
    }
    document.cookie = name + '=' + (value || '') + expires + '; path=/';
  }

  private getCookie(name) {
    var nameEQ = name + '=';
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
      var c = ca[i];
      while (c.charAt(0) == ' ') c = c.substring(1, c.length);
      if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
  }

  private eraseCookie(name) {
    document.cookie =
      name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
  }

  private startRefreshTokenTimer() {
    const helper = new JwtHelperService();

    const decodedToken = helper.decodeToken(this.userValue.jwtToken);

    const expires = new Date(decodedToken.exp * 1000);

    const timeout = expires.getTime() - Date.now() - 60 * 1000;

    this.refreshTokenTimeout = setTimeout(
      () => this.refreshToken().subscribe(),
      timeout
    );
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout);
  }
}
