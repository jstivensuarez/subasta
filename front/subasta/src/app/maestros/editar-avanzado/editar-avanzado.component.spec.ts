import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarAvanzadoComponent } from './editar-avanzado.component';

describe('EditarAvanzadoComponent', () => {
  let component: EditarAvanzadoComponent;
  let fixture: ComponentFixture<EditarAvanzadoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarAvanzadoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarAvanzadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
