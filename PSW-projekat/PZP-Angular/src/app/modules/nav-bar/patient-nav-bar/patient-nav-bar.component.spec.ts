import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PatientNavBarComponent } from './patient-nav-bar.component';

describe('PatientNavBarComponent', () => {
  let component: PatientNavBarComponent;
  let fixture: ComponentFixture<PatientNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientNavBarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
