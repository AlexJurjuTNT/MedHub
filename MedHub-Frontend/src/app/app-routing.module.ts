import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ChangeFormComponent, ChangePasswordFormComponent, ForgotPasswordFormComponent, LoginFormComponent} from './shared/components';
import {AuthGuardService} from './shared/services';
import {HomeComponent} from './pages/home/home.component';
import {ProfileComponent} from './pages/profile/profile.component';
import {TasksComponent} from './pages/tasks/tasks.component';
import {
  DxButtonModule,
  DxDataGridModule,
  DxFileManagerModule,
  DxFormModule,
  DxLoadIndicatorModule,
  DxPopupModule,
  DxSelectBoxModule,
  DxTagBoxModule,
  DxTextBoxModule
} from 'devextreme-angular';
import {PatientAddComponent} from './pages/patient-add/patient-add.component';
import {NgForOf, NgIf} from "@angular/common";
import {PatientListComponent} from './pages/patient-list/patient-list.component';
import {PatientTestsComponent} from "./pages/patient-tests/patient-tests.component";
import {TestTypeListComponent} from './pages/test-type-list/test-type-list.component';
import {TestRequestCreateComponent} from './pages/test-request-create/test-request-create.component';
import {TestResultCreateComponent} from "./pages/test-result-create/test-result-create.component";
import {TestResultView} from "./pages/test-result-view/test-result-view";
import {ClinicListComponent} from './pages/clinic-list/clinic-list.component';
import {DoctorListComponent} from './pages/doctor-list/doctor-list.component';
import {ChangeDefaultPasswordComponent} from "./shared/components/change-default-password/change-default-password.component";
import {PatientInfoComponent} from "./pages/patient-info/patient-info.component";
import {CreateTestTypePopupComponent} from "./components/test-type/create-test-type-popup/create-test-type-popup.component";
import {UpdateTestTypePopupComponent} from "./components/test-type/update-test-type-popup/update-test-type-popup.component";
import {CreateClinicPopupComponent} from "./components/clinic/create-clinic-popup/create-clinic-popup.component";
import {UpdateClinicPopupComponent} from "./components/clinic/update-clinic-popup/update-clinic-popup.component";
import {DoctorCreatePopupComponent} from "./components/doctor/doctor-create-popup/doctor-create-popup.component";
import {DoctorUpdatePopupComponent} from "./components/doctor/doctor-update-popup/doctor-update-popup.component";

const routes: Routes = [
  {
    path: 'pages/doctor-list',
    component: DoctorListComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'pages/clinic-list',
    component: ClinicListComponent,
    canActivate: [AuthGuardService]
  },
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
    path: 'change-default-password/:userId',
    component: ChangeDefaultPasswordComponent
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
    path: 'patient-info/:userId',
    component: PatientInfoComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {useHash: true}),
    DxDataGridModule, DxFormModule,
    DxLoadIndicatorModule,
    NgIf, DxSelectBoxModule,
    DxTagBoxModule,
    DxButtonModule,
    DxPopupModule,
    DxTextBoxModule,
    DxFileManagerModule,
    NgForOf],
  providers: [AuthGuardService],
  exports: [RouterModule],
  declarations: [
    HomeComponent,
    // Components created with ng generate are added automatically in app.module.ts and they cant be reached in components created with the template create view command
    // To be able to use components in generated views they need to be added here and removed from app.module.ts
    CreateTestTypePopupComponent,
    UpdateTestTypePopupComponent,
    CreateClinicPopupComponent,
    UpdateClinicPopupComponent,
    DoctorCreatePopupComponent,
    DoctorUpdatePopupComponent,
    ProfileComponent,
    TasksComponent,
    PatientAddComponent,
    PatientListComponent,
    TestTypeListComponent,
    TestRequestCreateComponent,
    ClinicListComponent,
    DoctorListComponent
  ]
})
export class AppRoutingModule {
}
