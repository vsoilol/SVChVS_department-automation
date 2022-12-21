import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { NgScrollbarModule } from 'ngx-scrollbar';
import { MaterialModule } from 'src/app/helpers/material.module';
import { MainModule } from 'src/app/lib/main/main.module';
import { EducationalProgramComponent } from './components/educational-program/educational-program.component';
import { TeacherRoutingModule } from './teacher-routing.module';

import { TeacherComponent } from './teacher.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ApplicationPipesModule } from 'src/app/pipes/application-pipes.module';
import { GuideComponent } from './components/guide/guide.component';

@NgModule({
  imports: [
    TeacherRoutingModule,
    MainModule,
    ReactiveFormsModule,
    NgbModule,
    FontAwesomeModule,
    MaterialModule,
    FormsModule,
    CommonModule,
    NgScrollbarModule,
    NgxSpinnerModule,
    ApplicationPipesModule,
  ],
  declarations: [TeacherComponent, EducationalProgramComponent, GuideComponent],
})
export class TeacherModule {}
