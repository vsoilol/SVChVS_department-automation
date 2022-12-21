import { Injectable } from '@angular/core';
import { Router, CanLoad, Route, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { Role } from '../models/Response/User';

import { AuthenticationService } from '../services/authentication.service';
import { LoginService } from '../services/login.service';

@Injectable({ providedIn: 'root' })
export class AuthLoginPageGuard implements CanLoad {
  constructor(
    private router: Router,
    private loginService: LoginService,
    private authenticationService: AuthenticationService
  ) {}

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    const user = this.authenticationService.userValue;

    if (!user) {
      return true;
    }

    this.loginService.redirectToPage();
    return false;
  }
}
