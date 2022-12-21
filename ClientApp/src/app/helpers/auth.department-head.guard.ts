import { Injectable } from '@angular/core';
import { Router, CanLoad, Route, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { Role } from '../models/Response/User';

import { AuthenticationService } from '../services/authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthDepartmentHeadGuard implements CanLoad {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {}

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    const user = this.authenticationService.userValue;
    if (user && user.role == Role.DepartmentHead) {
      return true;
    } else {
      this.router.navigate(['/miscellaneous/403'], {
        queryParams: { returnUrl: route.path },
        skipLocationChange: true,
      });
      return false;
    }
  }
}
