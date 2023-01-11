import { TestBed } from '@angular/core/testing';

import { DoctorExaminationEventService } from './doctor-examination-event.service';

describe('DoctorExaminationEventService', () => {
  let service: DoctorExaminationEventService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DoctorExaminationEventService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
