import {Component, Input, OnInit} from '@angular/core';
import {ClinicDto, CreateLaboratoryRequest, LaboratoryDto, LaboratoryService, UpdateLaboratoryRequest} from "../../../shared/services/swagger";

@Component({
  selector: 'app-laboratories-datagrid',
  templateUrl: './laboratories-datagrid.component.html',
  styleUrl: './laboratories-datagrid.component.scss'
})
// todo: when adding or updating laboratory, make it so datagrid is reloaded
export class LaboratoriesDatagridComponent implements OnInit {
  @Input() clinic: ClinicDto = {} as ClinicDto;
  createPopupVisible = false;
  updatePopupVisible = false;
  selectedRowKeys = [];
  selectedRow: LaboratoryDto = {} as LaboratoryDto;

  constructor(private laboratoryService: LaboratoryService) {
  }

  ngOnInit() {
  }

  createLaboratory(createLaboratoryRequest: CreateLaboratoryRequest) {
    this.laboratoryService.createLaboratory(createLaboratoryRequest).subscribe({
      next: () => {
        this.createPopupVisible = false;
      }
    })
  }

  updateLaboratory(updateLaboratoryRequest: UpdateLaboratoryRequest) {
    this.laboratoryService.updateLaboratory(this.selectedRow.id, updateLaboratoryRequest).subscribe({
      next: () => {
        this.updatePopupVisible = false;
      }
    })
  }

  onSelectionChanged(e: any) {
    this.selectedRow = e.selectedRowsData[0];
  }
}
