import { TestBed } from '@angular/core/testing';

import { TenderStatistcsService } from './tender-statistcs.service';

describe('TenderStatistcsService', () => {
  let service: TenderStatistcsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TenderStatistcsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
