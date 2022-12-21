import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MoreInformationComponent } from './more-information.component';
import {MoreInformationRoutingModule} from "./more-information-routing.module";



@NgModule({
  declarations: [
    MoreInformationComponent
  ],
  imports: [
    CommonModule,
    MoreInformationRoutingModule
  ]
})
export class MoreInformationModule { }
