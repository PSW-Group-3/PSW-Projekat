import { TestBed } from '@angular/core/testing';

import { PatientHealthService } from './patient-health.service';

describe('PatientHealthServiceService', () => {
  let service: PatientHealthService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatientHealthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
