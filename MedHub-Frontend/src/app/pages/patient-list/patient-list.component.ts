import {Component, OnInit} from '@angular/core';
import {PatientService} from "../../shared/services/swagger";
import {AuthService} from "../../shared/services";
import {Router} from "@angular/router";

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {

  dataSource: any;

  constructor(
    private patientService: PatientService,
    private authService: AuthService,
    private router: Router
  ) {
  }


  // todo: paging
  ngOnInit(): void {

    this.authService.getUser().then((e) => {
      if (e.data) {
        const user = e.data;
        console.log(e.data);

        this.patientService.getAllPatientsOfClinic(user.clinicId).subscribe({
          next: (result) => {
            this.dataSource = result.data;
          }
        })
      }
    });


  }

  viewPatient(patientId: number): void {
    this.router.navigate(['pages/patient-tests', patientId]);
  }
}
