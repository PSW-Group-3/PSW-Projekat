import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkoutsOverviewComponent } from './workouts-overview.component';

describe('WorkoutsOverviewComponent', () => {
  let component: WorkoutsOverviewComponent;
  let fixture: ComponentFixture<WorkoutsOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkoutsOverviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkoutsOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
