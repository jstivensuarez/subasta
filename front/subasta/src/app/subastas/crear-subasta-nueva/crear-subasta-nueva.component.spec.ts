import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearSubastaNuevaComponent } from './crear-subasta-nueva.component';

describe('CrearSubastaNuevaComponent', () => {
  let component: CrearSubastaNuevaComponent;
  let fixture: ComponentFixture<CrearSubastaNuevaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CrearSubastaNuevaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CrearSubastaNuevaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
