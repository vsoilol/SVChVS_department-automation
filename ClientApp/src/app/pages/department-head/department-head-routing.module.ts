import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartmentHeadComponent } from './department-head.component';

const routes: Routes = [
  {
    path: '',
    component: DepartmentHeadComponent,
    children: [
      {
        path: 'disciplinesTable',
        loadChildren: () =>
          import(
            '../department-head/pages/disciplines-table/disciplines-table.module'
          ).then((m) => m.DisciplinesTableModule),
      },
      {
        path: '',
        redirectTo: 'disciplinesTable',
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DepartmentHeadRoutingModule {}
