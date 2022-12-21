import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRoutingModule } from './login-routing.module';
import { LoginWithFioComponent } from './login-with-fio/login-with-fio.component';
import { LoginWithoutFioComponent } from './login-without-fio/login-without-fio.component';
import { MaterialModule } from 'src/app/helpers/material.module';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { MainModule } from 'src/app/lib/main/main.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { LoginComponent } from './login.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [
    LoginWithFioComponent,
    LoginWithoutFioComponent,
    LoginComponent,
  ],
  imports: [
    CommonModule,
    LoginRoutingModule,
    MainModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FlexLayoutModule,
    CommonModule,
    FontAwesomeModule,
  ],
})
export class LoginModule {}
