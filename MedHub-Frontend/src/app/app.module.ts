import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {SideNavInnerToolbarModule, SideNavOuterToolbarModule, SingleCardModule} from './layouts';
import {ChangePasswordFormModule, CreateAccountFormModule, FooterModule, LoginFormModule, ResetPasswordFormModule} from './shared/components';
import {AppInfoService, AuthService, ScreenService} from './shared/services';
import {UnauthenticatedContentModule} from './unauthenticated-content';
import {AppRoutingModule} from './app-routing.module';
import {ApiModule} from "./shared/services/swagger";
import {HttpClientModule} from "@angular/common/http";
import { PatientTestsComponent } from './pages/patient-tests/patient-tests.component';
import {DxDataGridModule, DxTemplateModule} from "devextreme-angular";
import {DxiColumnModule, DxoFilterRowModule, DxoPagerModule, DxoPagingModule} from "devextreme-angular/ui/nested";

@NgModule({
  declarations: [
    AppComponent,
    PatientTestsComponent
  ],
  imports: [
    BrowserModule,
    SideNavOuterToolbarModule,
    SideNavInnerToolbarModule,
    SingleCardModule,
    FooterModule,
    ResetPasswordFormModule,
    CreateAccountFormModule,
    ChangePasswordFormModule,
    LoginFormModule,
    UnauthenticatedContentModule,
    AppRoutingModule,
    ApiModule,
    HttpClientModule,
    DxDataGridModule,
    DxTemplateModule,
    DxiColumnModule,
    DxoFilterRowModule,
    DxoPagerModule,
    DxoPagingModule,
  ],
  providers: [
    AuthService,
    ScreenService,
    AppInfoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
