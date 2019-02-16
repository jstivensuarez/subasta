import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCiudadesComponent } from './admin-ciudades.component';

describe('AdminCiudadesComponent', () => {
  let component: AdminCiudadesComponent;
  let fixture: ComponentFixture<AdminCiudadesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminCiudadesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminCiudadesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
