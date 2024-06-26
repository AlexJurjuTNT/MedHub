import {Component, EventEmitter, Input, Output} from '@angular/core';
import {TestTypeDto} from "../../../shared/services/swagger";

@Component({
  selector: 'app-update-test-type-popup',
  templateUrl: './update-test-type-popup.component.html',
  styleUrl: './update-test-type-popup.component.scss'
})
export class UpdateTestTypePopupComponent {
  @Input() visible: boolean = false;
  @Input() selectedTestType: TestTypeDto = {} as TestTypeDto;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() updateTestType = new EventEmitter<TestTypeDto>();

  updateTestTypeFormData: any = {};

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();

    const updatedTestTypeDto: TestTypeDto = {
      name: this.updateTestTypeFormData.name,
      id: this.selectedTestType.id
    };

    this.updateTestType.emit(updatedTestTypeDto);
  }
}
