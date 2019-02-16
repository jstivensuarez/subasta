import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminRazasComponent } from './admin-razas.component';

describe('AdminRazasComponent', () => {
  let component: AdminRazasComponent;
  let fixture: ComponentFixture<AdminRazasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminRazasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminRazasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
