import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationStatisticComponent } from './examination-statistic.component';

describe('ExaminationStatisticComponent', () => {
  let component: ExaminationStatisticComponent;
  let fixture: ComponentFixture<ExaminationStatisticComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExaminationStatisticComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExaminationStatisticComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
