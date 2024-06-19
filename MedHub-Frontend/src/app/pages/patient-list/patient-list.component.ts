import {Component, OnInit} from '@angular/core';
import {PatientService, UserDto, UserService} from "../../shared/services/swagger";
import {AuthService} from "../../shared/services";
import {Router} from "@angular/router";

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  dataSource: any;
  doctor: UserDto = {} as UserDto;

  constructor(
    private patientService: PatientService,
    private userService: UserService,
    private authService: AuthService,
    private router: Router
  ) {
  }


  // todo: paging
  ngOnInit(): void {
    this.loadDoctorAndPatients();
  }

  viewPatient(patientId: number): void {
    this.router.navigate(['pages/patient-tests', patientId]);
  }

  deleteUser(currentUserId: number | null) {
    this.userService.deleteUser(currentUserId!).subscribe({});
    this.loadPatientsOfClinic();
  }

  private loadDoctorAndPatients() {
    this.loadCurrentUser();
    this.loadPatientsOfClinic();
  }

  private loadCurrentUser() {
    let result = this.authService.getUserSync();
    if (result.isOk) {
      this.doctor = result.data!;
    }
  }

  private loadPatientsOfClinic() {
    this.patientService.getAllPatientsOfClinic(this.doctor.clinicId).subscribe({
      next: (result) => {
        this.dataSource = result.data;
      }
    });
  }
}
