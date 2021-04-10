import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarImageUpdateComponent } from './car-image-update.component';

describe('CarImageUpdateComponent', () => {
  let component: CarImageUpdateComponent;
  let fixture: ComponentFixture<CarImageUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarImageUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarImageUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
