import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearPropietarioComponent } from './crear-propietario';

describe('CrearPropietarioComponent', () => {
  let component: CrearPropietarioComponent;
  let fixture: ComponentFixture<CrearPropietarioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CrearPropietarioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CrearPropietarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
