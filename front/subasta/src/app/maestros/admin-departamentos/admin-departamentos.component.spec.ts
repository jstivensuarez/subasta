import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminDepartamentosComponent } from './admin-departamentos.component';

describe('AdminDepartamentosComponent', () => {
  let component: AdminDepartamentosComponent;
  let fixture: ComponentFixture<AdminDepartamentosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminDepartamentosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminDepartamentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
