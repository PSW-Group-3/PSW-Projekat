import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PatientHealthScoreComponent } from './patient-health-score.component';

describe('PatientHealthScoreComponent', () => {
  let component: PatientHealthScoreComponent;
  let fixture: ComponentFixture<PatientHealthScoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientHealthScoreComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientHealthScoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
