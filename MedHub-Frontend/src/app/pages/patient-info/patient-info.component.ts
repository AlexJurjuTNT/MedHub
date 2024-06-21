import {Component, OnInit} from '@angular/core';
import {AddPatientDataDto, PatientDto, PatientService, UpdatePatientDto} from "../../shared/services/swagger";
import {ActivatedRoute} from "@angular/router";
import { format } from 'date-fns';

@Component({
  selector: 'app-patient-info',
  templateUrl: './patient-info.component.html',
  styleUrl: './patient-info.component.scss'
})
export class PatientInfoComponent implements OnInit {

  patient: PatientDto | null = null;
  userId: number = 0;
  colCountByScreen: object;

  updatePopupVisible: boolean = false;
  patientCopy: PatientDto | AddPatientDataDto = {} as PatientDto;

  constructor(
    private patientService: PatientService,
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

  showUpdatePopup() {
    this.updatePopupVisible = true;
    this.patientCopy = this.patient ? {...this.patient} : {userId: this.userId} as AddPatientDataDto;
  }

  hideUpdatePopup() {
    this.getPatientInfo();
    this.updatePopupVisible = false;
  }

  upsertPatientInfo($event: SubmitEvent) {
    $event.preventDefault();

    if (this.patient) {
      const updatePatientInfo: UpdatePatientDto = {
        cnp: this.patientCopy.cnp,
        dateOfBirth: this.patientCopy.dateOfBirth,
        weight: this.patientCopy.weight,
        height: this.patientCopy.height,
        gender: this.patientCopy.gender,
      };

      console.log(updatePatientInfo)

      this.patientService.updatePatientInformation(this.patient.id, updatePatientInfo).subscribe({
        next: () => {
          this.hideUpdatePopup();
        }
      });
    } else {
      const addPatientInfo: AddPatientDataDto = {
        userId: this.userId,
        cnp: this.patientCopy.cnp,
        dateOfBirth: format(new Date(this.patientCopy.dateOfBirth), 'yyyy-MM-dd'),
        weight: this.patientCopy.weight,
        height: this.patientCopy.height,
        gender: this.patientCopy.gender,
      };

      console.log(addPatientInfo)


      this.patientService.addPatientInformation(addPatientInfo).subscribe({
        next: () => {
          this.hideUpdatePopup();
        }
      });
    }
  }
}
