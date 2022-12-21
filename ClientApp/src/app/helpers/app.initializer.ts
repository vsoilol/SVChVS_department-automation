import { AuthenticationService } from '../services/authentication.service';

export function appInitializer(authenticationService: AuthenticationService) {
  return () =>
    new Promise((resolve) => {
      // attempt to refresh token on app start up to auto authenticate
      authenticationService.refreshToken().subscribe().add(resolve);
      //  this.router.events
      //    .pipe(filter((event) => event instanceof NavigationStart))
      //    .subscribe((_) => {
      //      console.log(_.url);
      //      // this.loginService.redirectToPage();
      //    });
    });
}
