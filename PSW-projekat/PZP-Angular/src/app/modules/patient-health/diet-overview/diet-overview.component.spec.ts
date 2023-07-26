import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DietOverviewComponent } from './diet-overview.component';

describe('DietOverviewComponent', () => {
  let component: DietOverviewComponent;
  let fixture: ComponentFixture<DietOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DietOverviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DietOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
