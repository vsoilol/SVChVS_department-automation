import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanDeactivate,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { switchMap, map, take } from 'rxjs/operators';

export interface IDeactivateComponent {
  canExit: () => Observable<boolean>;
}

@Injectable({ providedIn: 'root' })
export class DirtyCheckGuard implements CanDeactivate<IDeactivateComponent> {
  canDeactivate(
    component: IDeactivateComponent,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState?: RouterStateSnapshot
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    return component.canExit ? component.canExit() : true;

    // if (!component.canExit) {
    //   return of(true);
    // }

    // component.canExit().subscribe((value) => {
    //   return value;
    // });
  }
}
