import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomePatientComponent } from './homePatient.component';

describe('HomeComponent', () => {
  let component: HomePatientComponent;
  let fixture: ComponentFixture<HomePatientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomePatientComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomePatientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
