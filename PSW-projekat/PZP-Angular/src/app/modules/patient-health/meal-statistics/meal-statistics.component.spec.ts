import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealStatisticsComponent } from './meal-statistics.component';

describe('MealStatisticsComponent', () => {
  let component: MealStatisticsComponent;
  let fixture: ComponentFixture<MealStatisticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MealStatisticsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MealStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
