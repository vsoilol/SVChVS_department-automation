import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DepartmentHeadRoutingModule } from './department-head-routing.module';
import { DepartmentHeadComponent } from './department-head.component';
import { MainModule } from 'src/app/lib/main/main.module';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialModule } from 'src/app/helpers/material.module';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [DepartmentHeadComponent],
  imports: [
    CommonModule,
    MainModule,
    NgScrollbarModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    MaterialModule,
    FormsModule,
    CommonModule,
    NgxSpinnerModule,
    DepartmentHeadRoutingModule,
  ],
})
export class DepartmentHeadModule {}
