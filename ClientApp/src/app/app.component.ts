import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { OnlineStatusType } from 'ngx-online-status';
import { LoginService } from './services/login.service';
import 'src/js/app';
import { ActivatedRoute, NavigationEnd, Route } from '@angular/router';
import { filter } from 'rxjs/operators';
import {
  Router,
  NavigationStart,
  Event as NavigationEvent,
} from '@angular/router';
import { LoaderService } from './lib/main/loader/loader.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';
import { WindowsCanvasService } from './services/windows-canvas.service';
import {AuthenticationService} from "./services/authentication.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'ClientApp';
  isLoad: boolean = false;

  status: OnlineStatusType;
  onlineStatusCheck: any = OnlineStatusType;

  public loaderName: string = 'page-change-loader';
  public isShow: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(
    private loginService: LoginService,
    translate: TranslateService,
    private router: Router,
    public loader: LoaderService,
    private spinner: NgxSpinnerService,
    private route: ActivatedRoute,
    public windowCanvas: WindowsCanvasService,
    private authenticationService: AuthenticationService
  ) {
/*    const user = this.authenticationService.userValue;
    console.log(user, router)*/
    const subscription = this.router.events.subscribe((e) => {
      if (e instanceof NavigationStart) {
        //console.log('Start', e);
      }
      if (e instanceof NavigationEnd) {
        //console.log('End', e);
      }
    });
  }
}
