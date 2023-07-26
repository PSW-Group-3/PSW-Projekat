import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMealDialogComponent } from './add-meal-dialog.component';

describe('AddMealDialogComponent', () => {
  let component: AddMealDialogComponent;
  let fixture: ComponentFixture<AddMealDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddMealDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddMealDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
