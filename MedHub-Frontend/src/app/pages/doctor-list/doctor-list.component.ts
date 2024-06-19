import {Component, OnInit} from '@angular/core';
import {AuthenticationService, DoctorService, UserDto, UserRegisterDto} from "../../shared/services/swagger";

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css']
})
export class DoctorListComponent implements OnInit {

  dataSource: any;
  createDoctorPopupVisible: boolean = false;


  createDoctorFormData: any = {};

  doctorId: number = 0;
  deletePopupVisible: boolean = false;

  updatePopupVisible: boolean = false;
  updateDoctorFormData: any = {};
  selectedDoctor: UserDto = {} as UserDto;

  constructor(
    private doctorService: DoctorService,
    private authenticationService: AuthenticationService,
  ) {
  }

  ngOnInit(): void {
    this.getAllDoctors();
  }

  createDoctor($event: SubmitEvent) {
    $event.preventDefault();

    const addDoctorDto: UserRegisterDto = {
      username: this.createDoctorFormData.username,
      password: this.createDoctorFormData.password,
      email: this.createDoctorFormData.email,
      clinicId: this.createDoctorFormData.clinicId
    }

    this.authenticationService.registerDoctor(addDoctorDto).subscribe({
      next: result => {
        this.createDoctorPopupVisible = false;
        this.getAllDoctors();
      }
    })

  }

  openDeletePopup(doctorId: number) {
    this.doctorId = doctorId;
    this.deletePopupVisible = true;
  }

  openUpdatePopup(doctor: UserDto) {
    this.selectedDoctor = {...doctor};
    this.updatePopupVisible = true;
  }

  deleteDoctor() {
    this.doctorService.deleteDoctor(this.doctorId).subscribe({
      next: result => {
        this.deletePopupVisible = false;
        this.getAllDoctors();
      }
    })
  }

  updateDoctor($event: SubmitEvent) {
    $event.preventDefault();

    const updateDoctor: UserDto = {
      id: this.selectedDoctor.id,
      email: this.updateDoctorFormData.email,
      username: this.updateDoctorFormData.username,
      clinicId: this.updateDoctorFormData.clinicId
    }

    this.doctorService.updateDoctor(this.selectedDoctor.id, updateDoctor).subscribe({
      next: result => {
        this.updatePopupVisible = false;
        this.getAllDoctors();
      }
    })

  }

  private getAllDoctors() {
    this.doctorService.getAllDoctors().subscribe({
      next: result => {
        this.dataSource = result;
      }
    });
  }
}
