import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DisciplinesTableComponent } from './disciplines-table.component';
import { RouterModule } from '@angular/router';
import { MainModule } from 'src/app/lib/main/main.module';
import { AdditionalDisciplineInfoComponent } from './dialogs/additional-discipline-info/additional-discipline-info.component';
import { MaterialModule } from 'src/app/helpers/material.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [DisciplinesTableComponent, AdditionalDisciplineInfoComponent],
  imports: [
    CommonModule,
    MainModule,
    ReactiveFormsModule,
    MaterialModule,
    RouterModule.forChild([
      {
        path: '',
        component: DisciplinesTableComponent,
      },
    ]),
  ],
})
export class DisciplinesTableModule {}
