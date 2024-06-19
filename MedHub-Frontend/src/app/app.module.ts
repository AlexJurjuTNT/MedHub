import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {SideNavInnerToolbarModule, SideNavOuterToolbarModule, SingleCardModule} from './layouts';
import {ChangePasswordFormModule, CreateAccountFormModule, FooterModule, LoginFormModule, ResetPasswordFormModule} from './shared/components';
import {AppInfoService, AuthService, ScreenService} from './shared/services';
import {UnauthenticatedContentModule} from './unauthenticated-content';
import {AppRoutingModule} from './app-routing.module';
import {ApiModule} from "./shared/services/swagger";
import {HttpClientModule, provideHttpClient, withInterceptors} from "@angular/common/http";
import {PatientTestsComponent} from './pages/patient-tests/patient-tests.component';
import {DxButtonModule, DxDataGridModule, DxFileUploaderModule, DxTagBoxModule, DxTemplateModule} from "devextreme-angular";
import {DxiColumnModule, DxoFilterRowModule, DxoPagerModule, DxoPagingModule} from "devextreme-angular/ui/nested";
import {TestResultCreateComponent} from './pages/test-result-create/test-result-create.component';
import {TestResultView} from './pages/test-result-view/test-result-view';
import {httpTokenInterceptor} from "./shared/services/http-token.interceptor";

@NgModule({
  declarations: [
    AppComponent,
    PatientTestsComponent,
    TestResultCreateComponent,
    TestResultView
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
    DxButtonModule,
    DxFileUploaderModule,
    DxTagBoxModule,
  ],
  providers: [
    AuthService,
    ScreenService,
    AppInfoService,
    provideHttpClient(withInterceptors([httpTokenInterceptor]))
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
