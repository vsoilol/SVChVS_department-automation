import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { MiscellaneousComponent } from './miscellaneous.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { ServiceUnavailableComponent } from './service-unavailable/service-unavailable.component';

const routes: Routes = [
  {
    path: '',
    component: MiscellaneousComponent,
    children: [
      {
        path: '404',
        component: NotFoundComponent,
      },
      {
        path: '403',
        component: ForbiddenComponent,
      },
      {
        path: '503',
        component: ServiceUnavailableComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MiscellaneousRoutingModule {}
