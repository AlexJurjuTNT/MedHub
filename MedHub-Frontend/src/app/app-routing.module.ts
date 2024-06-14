import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ChangeFormComponent, ChangePasswordFormComponent, LoginFormComponent, ForgotPasswordFormComponent} from './shared/components';
import {AuthGuardService} from './shared/services';
import {HomeComponent} from './pages/home/home.component';
import {ProfileComponent} from './pages/profile/profile.component';
import {TasksComponent} from './pages/tasks/tasks.component';
import {DxButtonModule, DxDataGridModule, DxFormModule, DxLoadIndicatorModule, DxPopupModule, DxSelectBoxModule, DxTagBoxModule} from 'devextreme-angular';
import {PatientAddComponent} from './pages/patient-add/patient-add.component';
import {NgIf} from "@angular/common";
import {PatientListComponent} from './pages/patient-list/patient-list.component';
import {PatientTestsComponent} from "./pages/patient-tests/patient-tests.component";
import {TestTypeListComponent} from './pages/test-type-list/test-type-list.component';
import {TestRequestCreateComponent} from './pages/test-request-create/test-request-create.component';
import {TestResultCreateComponent} from "./pages/test-result-create/test-result-create.component";
import {TestResultView} from "./pages/test-result-view/test-result-view";

const routes: Routes = [
  {
    path: 'pages/test-request-create',
    component: TestRequestCreateComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'pages/test-type-list',
    component: TestTypeListComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'pages/patient-list',
    component: PatientListComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'pages/patient-tests/:id',
    component: PatientTestsComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'pages/patient-add',
    component: PatientAddComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'pages/test-result-view/:testResultId',
    component: TestResultView,
    canActivate: [AuthGuardService]
  },
  {
    path: 'pages/test-result-create/:testRequestId',
    component: TestResultCreateComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'tasks',
    component: TasksComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'login-form',
    component: LoginFormComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'reset-password',
    component: ForgotPasswordFormComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'change-password',
    component: ChangePasswordFormComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'change-password/:recoveryCode',
    component: ChangeFormComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true}), DxDataGridModule, DxFormModule, DxLoadIndicatorModule, NgIf, DxSelectBoxModule, DxTagBoxModule, DxButtonModule, DxPopupModule],
  providers: [AuthGuardService],
  exports: [RouterModule],
  declarations: [
    HomeComponent,
    ProfileComponent,
    TasksComponent,
    PatientAddComponent,
    PatientListComponent,
    TestTypeListComponent,
    TestRequestCreateComponent
  ]
})
export class AppRoutingModule {
}
