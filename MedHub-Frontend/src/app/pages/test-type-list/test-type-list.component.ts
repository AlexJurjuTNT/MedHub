import {Component, OnInit} from '@angular/core';
import {TestTypeService} from "../../shared/services/swagger";

@Component({
  selector: 'app-test-type-list',
  templateUrl: './test-type-list.component.html',
  styleUrls: ['./test-type-list.component.css']
})
export class TestTypeListComponent implements OnInit {

  dataSource: any;

  constructor(
    private testTypeService: TestTypeService
  ) {
  }

  // todo: paging
  ngOnInit(): void {
    this.testTypeService.getAllTestTypes().subscribe({
      next: data => {
        this.dataSource = data;
      }
    })
  }

}
