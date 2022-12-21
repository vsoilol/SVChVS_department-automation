import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from '../services/authentication.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private authenticationService: AuthenticationService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (
          [401, 403].includes(err.status) &&
          this.authenticationService.userValue
        ) {
          // auto logout if 401 or 403 response returned from api
          // this.authenticationService.logout();
        }

        if ([505, 503, 0].includes(err.status)) {
          this.router.navigate(['miscellaneous/503'], {
            skipLocationChange: true,
          });
        }

        //const error = (err && err.error && err.error.message) || err.statusText;
        //console.error(error);
        return throwError(err);
      })
    );
  }
}
