import { Component, EventEmitter, Input, Output } from '@angular/core';
import {CreateTestTypeRequest} from "../../../shared/services/swagger";

@Component({
  selector: 'app-create-test-type-popup',
  templateUrl: './create-test-type-popup.component.html',
  styleUrl: './create-test-type-popup.component.scss'
})
export class CreateTestTypePopupComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() createTestType = new EventEmitter<CreateTestTypeRequest>();

  createTestTypeFormData: any = {};

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();
    const createTestTypeRequest: CreateTestTypeRequest = {
      name: this.createTestTypeFormData.name
    };
    this.createTestType.emit(createTestTypeRequest);
  }
}
