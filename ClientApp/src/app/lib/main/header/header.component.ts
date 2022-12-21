import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {Role, User} from 'src/app/models/Response/User';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { LoaderService } from '../loader/loader.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  @Input() sidenav;

  public tooltipInfo: string = '';

  user: User;

  constructor(
    private authenticationService: AuthenticationService,
    private loader: LoaderService,
    private router: Router
  ) {
    this.authenticationService.user.subscribe((x) => {
      this.user = x;
      if (x) {
        this.tooltipInfo = `${this.user.surname} ${this.user.firstName} ${this.user.patronymic}`;
      }
    });
  }

  ngOnInit(): void {}

  public get role(): typeof Role {
    return Role;
  }

  logout() {
    this.loader.show();
    this.authenticationService.logout().subscribe((_) => {
      this.loader.hide();
    });
  }
}
