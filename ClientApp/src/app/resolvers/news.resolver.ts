import { Injectable } from '@angular/core';
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';
import { LoaderService } from '../lib/main/loader/loader.service';

@Injectable({
  providedIn: 'root',
})
export class NewsResolver implements Resolve<Observable<string>> {
  constructor(private loaderService: LoaderService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<string> {
    this.loaderService.show();
    return of('Route!').pipe(
      delay(5000),
      finalize(() => this.loaderService.hide())
    );
  }
}
