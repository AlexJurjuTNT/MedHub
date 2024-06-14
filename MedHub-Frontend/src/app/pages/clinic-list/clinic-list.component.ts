import {Component, OnInit} from '@angular/core';
import {AddClinicDto, ClinicDto, ClinicService} from "../../shared/services/swagger";

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

  private getAllClinics() {
    this.clinicService.getAllClinics().subscribe({
      next: result => {
        this.dataSource = result;
      }
    })
  }

  createClinic($event: Event) {
    $event.preventDefault();

    const addClinicDto: AddClinicDto = {
      name: this.createClinicFormData.name,
      location: this.createClinicFormData.location,
      sendgridApiKey: this.createClinicFormData.sendgridApiKey,
    }

    this.clinicService.createClinic(addClinicDto).subscribe({
      next: result => {
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
      next: result => {
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

    const updateClinic: ClinicDto = {
      id: this.selectedClinic.id,
      name: this.updateClinicFormData.name,
      location: this.updateClinicFormData.location,
      sendgridApiKey: this.updateClinicFormData.sendgridApiKey
    }

    this.clinicService.updateClinic(this.selectedClinic.id, updateClinic).subscribe({
      next: result => {
        this.updatePopupVisible = false;
        this.getAllClinics();
      }
    })
  }
}
