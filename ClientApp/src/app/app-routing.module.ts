import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers';
import { AuthAdminGuard } from './helpers/auth.admin.guard';
import { NotFoundComponent } from './pages/miscellaneous/not-found/not-found.component';
import { AuthTeacherGuard } from './helpers/auth.teacher.guard';
import { AuthDepartmentHeadGuard } from './helpers/auth.department-head.guard';
import { AuthLoginPageGuard } from './helpers/auth.login-page.guard';
import { NewsResolver } from './resolvers/news.resolver';
import { AppComponent } from './app.component';
import {MainPageGuard} from "./helpers/main-page.guard";
//import { NewsResolver } from './resolvers/news.resolver';

const routes: Routes = [
  {
    path: 'admin',
    loadChildren: () =>
      import('../app/pages/admin/admin.module').then((m) => m.AdminModule),
    canLoad: [AuthAdminGuard],
  },
  {
    path: 'teacher',
    loadChildren: () =>
      import('../app/pages/teacher/teacher.module').then(
        (m) => m.TeacherModule
      ),
    canLoad: [AuthTeacherGuard],
  },
  {
    path: 'departmentHead',
    loadChildren: () =>
      import('../app/pages/department-head/department-head.module').then(
        (m) => m.DepartmentHeadModule
      ),
    canLoad: [AuthDepartmentHeadGuard],
  },
  {
    path: 'authentication',
    loadChildren: () =>
      import('../app/pages/authentication/pages/login/login.module').then(
        (m) => m.LoginModule
      ),
    canLoad: [AuthLoginPageGuard],
  },
  {
    path: 'miscellaneous',
    loadChildren: () =>
      import('../app/pages/miscellaneous/miscellaneous.module').then(
        (m) => m.MiscellaneousModule
      ),
  },
  {
    path: '',
    loadChildren: () =>
      import('../app/pages/main-page/main-page.module').then(
        (m) => m.MainPageModule
      ),
  },
  {
    path: 'more-information',
    loadChildren: () =>
      import('../app/pages/more-information/more-information.module').then(
        (m) => m.MoreInformationModule
      ),
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
