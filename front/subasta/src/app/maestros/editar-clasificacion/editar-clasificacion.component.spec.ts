import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarClasificacionComponent } from './editar-clasificacion.component';

describe('EditarClasificacionComponent', () => {
  let component: EditarClasificacionComponent;
  let fixture: ComponentFixture<EditarClasificacionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarClasificacionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarClasificacionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
