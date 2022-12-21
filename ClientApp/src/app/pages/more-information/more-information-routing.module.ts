import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MoreInformationComponent} from "./more-information.component";

const routes: Routes = [
  {
    path: '',
    component: MoreInformationComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MoreInformationRoutingModule {}
