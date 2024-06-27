import {Component, Input} from '@angular/core';
import {ClinicDto, CreateLaboratoryRequest, LaboratoryDto, LaboratoryService, UpdateLaboratoryRequest} from "../../../shared/services/swagger";
import {NotificationService} from "../../../shared/services/notification.service";

@Component({
  selector: 'app-laboratories-datagrid',
  templateUrl: './laboratories-datagrid.component.html',
  styleUrl: './laboratories-datagrid.component.scss'
})
// todo: when adding or updating laboratory, make it so datagrid is reloaded
export class LaboratoriesDatagridComponent {
  @Input() clinic: ClinicDto = {} as ClinicDto;
  createPopupVisible = false;
  updatePopupVisible = false;
  selectedRowKeys = [];
  selectedRow: LaboratoryDto = {} as LaboratoryDto;

  constructor(
    private laboratoryService: LaboratoryService,
    private notificationService: NotificationService,
  ) {
  }

  private refreshLaboratories(){
  }

  createLaboratory(createLaboratoryRequest: CreateLaboratoryRequest) {
    this.laboratoryService.createLaboratory(createLaboratoryRequest).subscribe({
      next: (newLaboratory) => {
        this.createPopupVisible = false;
        this.clinic.laboratories.push(newLaboratory);
        this.notificationService.success("Laboratory created successfully");
      },
      error: (error) => {
        this.notificationService.error("Error creating laboratory: " + error.message);
      }
    });
  }

  // alternative would be use laboratory service to get laboratories again
  updateLaboratory(updateLaboratoryRequest: UpdateLaboratoryRequest) {
    this.laboratoryService.updateLaboratory(this.selectedRow.id, updateLaboratoryRequest).subscribe({
      next: (updatedLaboratory) => {
        this.updatePopupVisible = false;
        const index = this.clinic.laboratories.findIndex(lab => lab.id === updatedLaboratory.id);
        if (index !== -1) {
          this.clinic.laboratories[index] = updatedLaboratory;
        }
        this.notificationService.success("Laboratory updated successfully");
      },
      error: (error) => {
        this.notificationService.error("Error updating laboratory: " + error.message);
      }
    });
  }


  onSelectionChanged(e: any) {
    this.selectedRow = e.selectedRowsData[0];
  }
}
