import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminLoyautComponent } from './admin-loyaut.component';

describe('AdminLoyautComponent', () => {
  let component: AdminLoyautComponent;
  let fixture: ComponentFixture<AdminLoyautComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminLoyautComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminLoyautComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
