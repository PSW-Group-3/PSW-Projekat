import { TestBed } from '@angular/core/testing';

import { MealStatisticsService } from './meal-statistics.service';

describe('MealStatisticsService', () => {
  let service: MealStatisticsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MealStatisticsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
