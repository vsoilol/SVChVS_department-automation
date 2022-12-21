import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SidenavListComponent } from './sidenav-list/sidenav-list.component';
import { LoadingSpinnerComponent } from './loader/loading-spinner/loading-spinner.component';

import { MaterialModule } from 'src/app/helpers/material.module';

import { HeaderComponent } from './header/header.component';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { SmartTableComponent } from './smart-table/smart-table.component';
import { CdkTableModule } from '@angular/cdk/table';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { InformationDialogComponent } from './information-dialog/information-dialog.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { DataTableComponent } from './data-table/data-table.component';
import { PickerComponent } from './picker/picker.component';
import { PickerDropdownComponent } from './picker/picker-dropdown/picker-dropdown.component';
import { PickerHeaderMultiComponent } from './picker/picker-header-multi/picker-header-multi.component';
import { PickerHeaderSingleComponent } from './picker/picker-header-single/picker-header-single.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { FooterComponent } from './footer/footer.component';

const COMPONENTS = [
  SidenavListComponent,
  LoadingSpinnerComponent,
  HeaderComponent,
  SmartTableComponent,
  InformationDialogComponent,
  DataTableComponent,
  PickerComponent,
  PickerDropdownComponent,
  PickerHeaderMultiComponent,
  PickerHeaderSingleComponent,
  SpinnerComponent,
  FooterComponent,
];

@NgModule({
  declarations: [...COMPONENTS],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MaterialModule,
    CdkTableModule,
    ReactiveFormsModule,
    NgbModule,
    FontAwesomeModule,
    NgxSpinnerModule,
  ],
  exports: [...COMPONENTS],
})
export class MainModule {}
