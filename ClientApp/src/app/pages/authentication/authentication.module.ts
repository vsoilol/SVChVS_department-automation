import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgScrollbarModule } from 'ngx-scrollbar';
import { MaterialModule } from 'src/app/helpers/material.module';
import { MainModule } from 'src/app/lib/main/main.module';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { AuthenticationComponent } from './authentication.component';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './pages/login/login.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  imports: [
    AuthenticationRoutingModule,
    NgScrollbarModule,
    MainModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FlexLayoutModule,
    CommonModule,
  ],
  declarations: [AuthenticationComponent, LoginComponent],
})
export class AuthenticationModule {}
