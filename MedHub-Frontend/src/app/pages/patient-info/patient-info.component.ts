import {Component, OnInit} from '@angular/core';
import {CreatePatientInformationRequest, PatientInformationDto, PatientInformationService, UpdatePatientInformationRequest} from "../../shared/services/swagger";
import {ActivatedRoute} from "@angular/router";
import {format} from 'date-fns';

@Component({
  selector: 'app-patient-info',
  templateUrl: './patient-info.component.html',
  styleUrl: './patient-info.component.scss'
})
export class PatientInfoComponent implements OnInit {

  patient: PatientInformationDto | null = null;
  userId: number = 0;
  colCountByScreen: object;

  updatePopupVisible: boolean = false;
  patientCopy: PatientInformationDto | CreatePatientInformationRequest = {} as PatientInformationDto;

  constructor(
    private patientService: PatientInformationService,
    private route: ActivatedRoute,
  ) {
    this.colCountByScreen = {
      xs: 1,
      sm: 2,
      md: 3,
      lg: 4
    };
  }

  ngOnInit(): void {
    this.getPatientInfo();
  }

  showUpdatePopup() {
    this.updatePopupVisible = true;
    this.patientCopy = this.patient ? {...this.patient} : {userId: this.userId} as CreatePatientInformationRequest;
  }

  hideUpdatePopup() {
    this.getPatientInfo();
    this.updatePopupVisible = false;
  }

  upsertPatientInfo($event: SubmitEvent) {
    $event.preventDefault();

    if (this.patient) {
      const updatePatientInformationRequest: UpdatePatientInformationRequest = {
        cnp: this.patientCopy.cnp,
        dateOfBirth: this.patientCopy.dateOfBirth,
        weight: this.patientCopy.weight,
        height: this.patientCopy.height,
        gender: this.patientCopy.gender,
      };

      console.log(updatePatientInformationRequest)

      this.patientService.updatePatientInformation(this.patient.id, updatePatientInformationRequest).subscribe({
        next: () => {
          this.hideUpdatePopup();
        }
      });
    } else {
      const createPatientInformationRequest: CreatePatientInformationRequest = {
        userId: this.userId,
        cnp: this.patientCopy.cnp,
        dateOfBirth: format(new Date(this.patientCopy.dateOfBirth), 'yyyy-MM-dd'),
        weight: this.patientCopy.weight,
        height: this.patientCopy.height,
        gender: this.patientCopy.gender,
      };

      console.log(createPatientInformationRequest)


      this.patientService.addPatientInformation(createPatientInformationRequest).subscribe({
        next: () => {
          this.hideUpdatePopup();
        }
      });
    }
  }

  private getPatientInfo() {
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('userId'));

      this.patientService.getPatientInformationForUser(this.userId).subscribe({
        next: result => {
          this.patient = result;
        },
        error: () => {
          this.patient = null;
        }
      });
    });
  }
}
