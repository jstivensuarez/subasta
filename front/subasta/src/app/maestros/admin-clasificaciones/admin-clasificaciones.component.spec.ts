import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminClasificacionesComponent } from './admin-clasificaciones.component';

describe('AdminClasificacionesComponent', () => {
  let component: AdminClasificacionesComponent;
  let fixture: ComponentFixture<AdminClasificacionesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminClasificacionesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminClasificacionesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
