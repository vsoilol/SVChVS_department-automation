import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { appInitializer, ErrorInterceptor, JwtInterceptor } from './helpers';
import { MaterialModule } from './helpers/material.module';

import { AuthenticationService } from './services/authentication.service';

import { MainModule } from './lib/main/main.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { OnlineStatusModule } from 'ngx-online-status';

import { MatPaginatorIntl } from '@angular/material/paginator';
import { MatPaginationIntlService } from './lib/pagination-translate/custom-mat-paginator-intl.service';

import {
  TranslateLoader,
  TranslateModule as NgxTranslateModule,
} from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MainModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    MaterialModule,
    ReactiveFormsModule,
    NgScrollbarModule,
    FontAwesomeModule,
    OnlineStatusModule,
    NgxTranslateModule.forRoot({
      defaultLanguage: 'ru',
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializer,
      multi: true,
      deps: [AuthenticationService],
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    {
      provide: MatPaginatorIntl,
      useClass: MatPaginationIntlService,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
