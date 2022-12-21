import { Injectable } from '@angular/core';
import { Router, CanLoad, Route, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { Role } from '../models/Response/User';

import { AuthenticationService } from '../services/authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthTeacherGuard implements CanLoad {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {}

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    const user = this.authenticationService.userValue;
    if (user && user.role == Role.Teacher) {
      // logged in so return true
      return true;
    } else {
      // not logged in so redirect to login page with the return url
      this.router.navigate(['/miscellaneous/403'], {
        queryParams: { returnUrl: route.path },
        skipLocationChange: true,
      });
      return false;
    }
  }
}
