import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarPropietariosComponent } from './listar-propietarios.component';

describe('ListarClientesComponent', () => {
  let component: ListarPropietariosComponent;
  let fixture: ComponentFixture<ListarPropietariosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListarPropietariosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListarPropietariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
