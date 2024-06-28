import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CreateClinicRequest} from "../../../shared/services/swagger";

@Component({
  selector: 'app-create-clinic-popup',
  templateUrl: './create-clinic-popup.component.html'
})
export class CreateClinicPopupComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() createClinic = new EventEmitter<CreateClinicRequest>();

  createClinicFormData: any = {};

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: Event) {
    $event.preventDefault();
    const createClinicRequest: CreateClinicRequest = {
      name: this.createClinicFormData.name,
      location: this.createClinicFormData.location,
      sendgridApiKey: this.createClinicFormData.sendgridApiKey,
      email: this.createClinicFormData.email
    };
    this.createClinic.emit(createClinicRequest);
  }
}
