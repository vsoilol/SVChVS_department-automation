import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { LoginWithFioComponent } from './login-with-fio/login-with-fio.component';
import { LoginWithoutFioComponent } from './login-without-fio/login-without-fio.component';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
    children: [
      {
        path: 'withFIO',
        component: LoginWithFioComponent,
      },
      {
        path: 'withoutFIO',
        component: LoginWithoutFioComponent,
      },
      {
        path: '',
        redirectTo: 'withoutFIO',
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LoginRoutingModule {}
