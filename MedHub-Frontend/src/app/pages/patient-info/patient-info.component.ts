import {Component, OnInit} from '@angular/core';
import {PatientDto, PatientService, UpdatePatientDto} from "../../shared/services/swagger";
import {ActivatedRoute} from "@angular/router";


@Component({
  selector: 'app-patient-info',
  templateUrl: './patient-info.component.html',
  styleUrl: './patient-info.component.scss'
})
export class PatientInfoComponent implements OnInit {

  patient: PatientDto = {} as PatientDto;
  userId: number = 0;
  colCountByScreen: object;

  updatePopupVisible: boolean = false;
  patientCopy: PatientDto = {} as PatientDto;

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
          console.log(this.patient)
        }
      })
    });
  }

  showUpdatePopup() {
    this.updatePopupVisible = true;
    this.patientCopy = {...this.patient};
  }

  hideUpdatePopup() {
    this.getPatientInfo();
    this.updatePopupVisible = false;
  }

  updatePatientInfo($event: SubmitEvent) {
    $event.preventDefault()

    const updatePatientInfo: UpdatePatientDto = {
      cnp: this.patientCopy.cnp,
      dateOfBirth: this.patientCopy.dateOfBirth,
      weight: this.patientCopy.weight,
      height: this.patientCopy.height,
      gender: this.patientCopy.gender,
    }

    this.patientService.updatePatientInformation(this.patient.id, updatePatientInfo).subscribe({
      next: () => {
        this.hideUpdatePopup();
      }
    })
  }
}
