import {Component, OnInit} from '@angular/core';
import {AddClinicDto, ClinicDto, ClinicService, TestTypeDto, UpdateClinicDto} from "../../shared/services/swagger";

@Component({
  selector: 'app-clinic-list',
  templateUrl: './clinic-list.component.html',
  styleUrls: ['./clinic-list.component.css']
})
export class ClinicListComponent implements OnInit {

  createClinicPopupVisible: boolean = false;
  createClinicFormData: any = {};
  dataSource: any;

  deletePopupVisible: boolean = false;
  clinicId: number = 0;

  updatePopupVisible: boolean = false;
  updateClinicFormData: any = {};
  selectedClinic: ClinicDto = {} as ClinicDto;

  constructor(
    private clinicService: ClinicService,
  ) {
  }

  ngOnInit(): void {
    this.getAllClinics();
  }

  createClinic($event: Event) {
    $event.preventDefault();

    const addClinicDto: AddClinicDto = {
      name: this.createClinicFormData.name,
      location: this.createClinicFormData.location,
      sendgridApiKey: this.createClinicFormData.sendgridApiKey,
      email: this.createClinicFormData.email
    }

    this.clinicService.createClinic(addClinicDto).subscribe({
      next: () => {
        this.createClinicPopupVisible = false;
        this.getAllClinics();
      }
    })
  }

  openDeletePopup(id: number) {
    this.clinicId = id;
    this.deletePopupVisible = true;
  }

  deleteClinic() {
    this.clinicService.deleteClinic(this.clinicId).subscribe({
      next: () => {
        this.deletePopupVisible = false;
        this.getAllClinics();
      }
    })
  }

  openUpdatePopup(clinic: ClinicDto) {
    this.selectedClinic = {...clinic};
    this.updatePopupVisible = true;
  }

  updateClinic($event: SubmitEvent) {
    $event.preventDefault();

    const updateClinic: UpdateClinicDto = {
      id: this.selectedClinic.id,
      name: this.updateClinicFormData.name,
      location: this.updateClinicFormData.location,
      sendgridApiKey: this.updateClinicFormData.sendgridApiKey,
      email: this.updateClinicFormData.email
    }

    this.clinicService.updateClinic(this.selectedClinic.id, updateClinic).subscribe({
      next: () => {
        this.updatePopupVisible = false;
        this.getAllClinics();
      }
    })
  }

  private getAllClinics() {
    this.clinicService.getAllClinics().subscribe({
      next: result => {
        this.dataSource = result;
      }
    })
  }

  getTestTypeNames(testTypes: TestTypeDto[]): string {
    return testTypes.map(testType => testType.name).join(', ');
  }
}
