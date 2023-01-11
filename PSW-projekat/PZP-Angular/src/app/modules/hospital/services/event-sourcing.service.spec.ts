import { TestBed } from '@angular/core/testing';

import { EventSourcingService } from './event-sourcing.service';

describe('EventSourcingService', () => {
  let service: EventSourcingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventSourcingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
