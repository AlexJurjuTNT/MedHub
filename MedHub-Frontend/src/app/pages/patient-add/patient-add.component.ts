import {Component, OnInit} from '@angular/core';
import {AuthenticationService, PatientRegisterDto} from "../../shared/services/swagger";
import {AuthService} from "../../shared/services";

@Component({
  selector: 'app-patient-add',
  templateUrl: './patient-add.component.html',
  styleUrls: ['./patient-add.component.css']
})
export class PatientAddComponent implements OnInit {
  loading = false;
  formData: any = {};

  constructor(
    private authenticationService: AuthenticationService,
    private authService: AuthService,
  ) {
  }

  ngOnInit(): void {
  }

  async onSubmit($event: SubmitEvent) {
    $event.preventDefault();

    const user = await this.authService.getUser();

    if (user.isOk) {
      const registerPatientRequest: PatientRegisterDto = {
        email: this.formData.email,
        clinicId: user.data?.clinicId!,
      };

      this.loading = true;

      this.authenticationService.registerPatient(registerPatientRequest).subscribe({
        next: result => {
          alert("Created new patient");
          location.reload();
        },
        error: err => {
          alert("Failed to create new patient");
        }
      })

      this.loading = false;

    } else {
      console.error('Failed to retrieve user data');
    }
  }

}
