import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { NgScrollbarModule } from 'ngx-scrollbar';
import { ErrorInterceptor, JwtInterceptor } from 'src/app/helpers';
import { MaterialModule } from 'src/app/helpers/material.module';
import { InterceptorService } from 'src/app/lib/main/loader/interceptor.service';
import { MainModule } from 'src/app/lib/main/main.module';
import { TeacherComponent } from '../teacher/teacher.component';

import { AdminRoutingModule } from './admin-routing.module';

import { AdminComponent } from './admin.component';
import { AllUsersComponent } from './components/all-users/all-users.component';
import { TeachersTableComponent } from './components/teachers-table/teachers-table.component';

@NgModule({
  imports: [
    AdminRoutingModule,
    MainModule,
    NgScrollbarModule,
    ReactiveFormsModule,
    NgbModule,
    FontAwesomeModule,
    MaterialModule,
    FormsModule,
    CommonModule,
  ],
  declarations: [AdminComponent, AllUsersComponent, TeachersTableComponent],
})
export class AdminModule {}
