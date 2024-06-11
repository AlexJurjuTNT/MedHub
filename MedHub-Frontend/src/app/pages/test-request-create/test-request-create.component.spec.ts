import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestRequestCreateComponent } from './test-request-create.component';

describe('TestRequestCreateComponent', () => {
  let component: TestRequestCreateComponent;
  let fixture: ComponentFixture<TestRequestCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestRequestCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestRequestCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
