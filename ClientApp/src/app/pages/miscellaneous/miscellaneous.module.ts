import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgScrollbarModule } from 'ngx-scrollbar';
import { MaterialModule } from 'src/app/helpers/material.module';
import { MainModule } from 'src/app/lib/main/main.module';

import { MiscellaneousRoutingModule } from './miscellaneous-routing.module';
import { MiscellaneousComponent } from './miscellaneous.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServiceUnavailableComponent } from './service-unavailable/service-unavailable.component';

@NgModule({
  imports: [
    MiscellaneousRoutingModule,
    NgScrollbarModule,
    MainModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FlexLayoutModule,
  ],
  declarations: [MiscellaneousComponent, NotFoundComponent, ServiceUnavailableComponent],
})
export class MiscellaneousModule {}
