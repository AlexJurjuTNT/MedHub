import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {LaboratoryDto, LoadResult, TestTypeDto, TestTypeService, UpdateLaboratoryRequest} from "../../../shared/services/swagger";

@Component({
  selector: 'app-laboratory-update-popup',
  templateUrl: './laboratory-update-popup.component.html',
  styleUrl: './laboratory-update-popup.component.scss'
})
export class LaboratoryUpdatePopupComponent implements OnInit {

  @Input() visible: boolean = false;
  @Input() selectedLaboratory: LaboratoryDto = {} as LaboratoryDto;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() updatedLaboratory = new EventEmitter<UpdateLaboratoryRequest>();

  formData: any = {};
  testTypes: TestTypeDto[] = [];
  selectedTestTypesIds: number[] = [];

  constructor(private testTypeService: TestTypeService) {
  }

  ngOnInit(): void {
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

    const updateLaboratoryRequest: UpdateLaboratoryRequest = {
      location: this.formData.location,
      clinicId: this.formData.clinicId,
      testTypesId: this.selectedTestTypesIds
    };

    this.updatedLaboratory.emit(updateLaboratoryRequest);
  }

}
