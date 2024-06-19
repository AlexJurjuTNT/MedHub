import {ModuleWithProviders, NgModule, Optional, SkipSelf} from '@angular/core';
import {Configuration} from './configuration';
import {HttpClient} from '@angular/common/http';


import {AuthenticationService} from './api/authentication.service';
import {ClinicService} from './api/clinic.service';
import {DoctorService} from './api/doctor.service';
import {PatientService} from './api/patient.service';
import {TestRequestService} from './api/testRequest.service';
import {TestResultService} from './api/testResult.service';
import {TestTypeService} from './api/testType.service';
import {UserService} from './api/user.service';

@NgModule({
    imports: [],
    declarations: [],
    exports: [],
    providers: [
        AuthenticationService,
        ClinicService,
        DoctorService,
        PatientService,
        TestRequestService,
        TestResultService,
        TestTypeService,
        UserService]
})
export class ApiModule {
    constructor(@Optional() @SkipSelf() parentModule: ApiModule,
                @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
                'See also https://github.com/angular/angular/issues/20575');
        }
    }

    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders<ApiModule> {
        return {
            ngModule: ApiModule,
            providers: [{provide: Configuration, useFactory: configurationFactory}]
        };
    }
}
