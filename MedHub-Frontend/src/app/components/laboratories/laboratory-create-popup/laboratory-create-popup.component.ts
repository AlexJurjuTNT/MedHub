import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ClinicDto, CreateLaboratoryRequest, LoadResult, TestTypeDto, TestTypeService} from "../../../shared/services/swagger";

@Component({
  selector: 'app-laboratory-create-popup',
  templateUrl: './laboratory-create-popup.component.html',
  styleUrl: './laboratory-create-popup.component.scss'
})
export class LaboratoryCreatePopupComponent {
  @Input() visible: boolean = false;
  @Input() clinic: ClinicDto = {} as ClinicDto;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() createLaboratory = new EventEmitter<CreateLaboratoryRequest>();

  formData: any = {}
  testTypes: TestTypeDto[] = [];
  selectedTestTypesIds: number[] = [];

  constructor(private testTypeService: TestTypeService) {
    this.testTypeService.getAllTestTypes().subscribe({
      next: (result: LoadResult) => {
        this.testTypes = result.data!;
      }
    })
  }


  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();

    const createLaboratoryRequest: CreateLaboratoryRequest = {
      location: this.formData.location,
      clinicId: this.clinic.id,
      testTypesId: this.selectedTestTypesIds
    }

    this.createLaboratory.emit(createLaboratoryRequest);
  }

}
