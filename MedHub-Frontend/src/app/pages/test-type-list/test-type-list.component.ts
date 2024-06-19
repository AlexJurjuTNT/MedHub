import {Component, OnInit} from '@angular/core';
import {AddTestTypeDto, TestTypeDto, TestTypeService} from "../../shared/services/swagger";

@Component({
  selector: 'app-test-type-list',
  templateUrl: './test-type-list.component.html',
  styleUrls: ['./test-type-list.component.css']
})
export class TestTypeListComponent implements OnInit {

  dataSource: any;
  createTestTypePopupVisible = false;
  newTestTypeName: string = '';

  deletePopupVisible: boolean = false;
  testTypeIdToDelete: number = 0;

  updateTestTypeInfo: TestTypeDto = {} as TestTypeDto;
  updatePopupVisible: boolean = false;

  constructor(
    private testTypeService: TestTypeService
  ) {
  }

  // todo: paging
  ngOnInit(): void {
    this.getAllTestTypes();
  }

  showNewTestTypePopup() {
    this.newTestTypeName = ''; // Clear the input field
    this.createTestTypePopupVisible = true;
  }

  saveTestType() {
    const addTestTypeDto: AddTestTypeDto = {
      name: this.newTestTypeName
    }

    this.testTypeService.createTestType(addTestTypeDto).subscribe({
      next: result => {
        this.createTestTypePopupVisible = false;
        this.getAllTestTypes();
      }
    })
  }

  openDeletePopup(testTypeId: number) {
    this.testTypeIdToDelete = testTypeId;
    this.deletePopupVisible = true;
  }

  deleteTestType() {
    this.testTypeService.deleteTestType(this.testTypeIdToDelete).subscribe({
      next: result => {
        this.getAllTestTypes();
        this.deletePopupVisible = false;
      },
      error: err => {

      }
    })
  }

  openUpdatePopup(testType: TestTypeDto) {
    this.updateTestTypeInfo = {...testType};
    this.newTestTypeName = testType.name;
    this.updatePopupVisible = true;
  }

  updateTestType() {
    const updatedTestTypeDto: TestTypeDto = {
      name: this.newTestTypeName,
      id: this.updateTestTypeInfo.id
    };

    this.testTypeService.updateTestType(this.updateTestTypeInfo.id, updatedTestTypeDto).subscribe({
      next: result => {
        this.updatePopupVisible = false;
        this.getAllTestTypes();
      }
    })

  }

  private getAllTestTypes() {
    this.testTypeService.getAllTestTypes().subscribe({
      next: data => {
        this.dataSource = data;
      }
    });
  }
}
