import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGymworkoutDialogComponent } from './add-gymworkout-dialog.component';

describe('AddGymworkoutDialogComponent', () => {
  let component: AddGymworkoutDialogComponent;
  let fixture: ComponentFixture<AddGymworkoutDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddGymworkoutDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddGymworkoutDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
