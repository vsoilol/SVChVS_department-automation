import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers';
import { AuthAdminGuard } from './helpers/auth.admin.guard';
import { NotFoundComponent } from './pages/miscellaneous/not-found/not-found.component';
import { AuthTeacherGuard } from './helpers/auth.teacher.guard';
import { AuthDepartmentHeadGuard } from './helpers/auth.department-head.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'authentication',
    pathMatch: 'full',
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
