import {Component, OnInit} from '@angular/core';
import {PatientDto, PatientService} from "../../shared/services/swagger";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-patient-info',
  templateUrl: './patient-info.component.html',
  styleUrl: './patient-info.component.scss'
})
export class PatientInfoComponent implements OnInit {

  patient: PatientDto = {} as PatientDto;
  userId: number = 0;
  colCountByScreen: object;

  constructor(
    private patientService: PatientService,
    private route: ActivatedRoute,
    private router: Router
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


}
