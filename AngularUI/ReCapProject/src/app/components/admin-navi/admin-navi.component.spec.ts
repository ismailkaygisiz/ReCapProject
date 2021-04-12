import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminNaviComponent } from './admin-navi.component';

describe('AdminNaviComponent', () => {
  let component: AdminNaviComponent;
  let fixture: ComponentFixture<AdminNaviComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminNaviComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminNaviComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
