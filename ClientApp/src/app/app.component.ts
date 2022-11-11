import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { OnlineStatusType } from 'ngx-online-status';
import { LoginService } from './services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'ClientApp';

  status: OnlineStatusType;
  onlineStatusCheck: any = OnlineStatusType;

  constructor(private loginService: LoginService, translate: TranslateService) {
    this.loginService.redirectToPage();
  }
}
