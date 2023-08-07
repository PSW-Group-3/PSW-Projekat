import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWaterDialogComponent } from './add-water-dialog.component';

describe('AddWaterDialogComponent', () => {
  let component: AddWaterDialogComponent;
  let fixture: ComponentFixture<AddWaterDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddWaterDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddWaterDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
