export * from './authentication.service';
import {AuthenticationService} from './authentication.service';
import {ClinicService} from './clinic.service';
import {DoctorService} from './doctor.service';
import {LaboratoryService} from './laboratory.service';
import {PatientService} from './patient.service';
import {TestRequestService} from './testRequest.service';
import {TestResultService} from './testResult.service';
import {TestTypeService} from './testType.service';
import {UserService} from './user.service';

export * from './clinic.service';

export * from './doctor.service';

export * from './laboratory.service';

export * from './patient.service';

export * from './testRequest.service';

export * from './testResult.service';

export * from './testType.service';

export * from './user.service';

export const APIS = [AuthenticationService, ClinicService, DoctorService, LaboratoryService, PatientService, TestRequestService, TestResultService, TestTypeService, UserService];
